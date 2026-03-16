using System;
using INTERCAL.Runtime;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

using System.Reflection;

namespace INTERCAL
{
    public class CompilationContext
    {
        public enum AssemblyType
        {
            library, exe, winexe
        };

        StringBuilder source = new StringBuilder();

        //This map is used to map abstains to labels in the runtime.  The
        //compiler is smart enough to only emit abstain guards for statements
        //that might possibly be abstained.  At least I think it is - maybe 
        //that's a source of bugs... 
        public readonly static Dictionary<string, Type> AbstainMap = new Dictionary<string, Type>();

        //What program are we compiling?
        public Program program;

        //What is the build target?
        public string assemblyName;
        public string sourceFile;
        public AssemblyType assemblyType;
        public bool debugBuild = false;
        public bool skipPoliteness = false;
        public bool Verbose = false;

        //What will the base class be for the generated type?
        public string baseClass = "System.Object";

        //Which assemblies are we referencing?
        public ExportList[] references;

        //Which labels in this assembly will be turned into public
        //entry points?
        public Dictionary<string, bool> publicLabels;

        //public PRNG, mostly used for E774
        public Random random = new Random();

        //if this is set to false then E774 is never emitted
        public bool Buggy = true;

        //if this program references external instance classes I don't want to
        //create a new one at every method call.  Instead this compiler will
        //emit properties that lazy-instantiate the requested classes.  This
        //List is filled up by NextStatement::EmitExternalCall and is
        //then used to generate the private properties.
        public List<string> ExternalReferences = new List<string>();

        // Tracks all INTERCAL variable names seen during compilation for debug locals
        public HashSet<string> DebugVariables = new HashSet<string>();

        // Tracks return labels for the goto-based NEXT state machine
        public int NextReturnLabelCounter = 0;
        public List<int> NextReturnLabels = new List<int>();

        static CompilationContext()
        {
            AbstainMap["NEXTING"] = typeof(Statement.NextStatement);
            AbstainMap["FORGETTING"] = typeof(Statement.ForgetStatement);
            AbstainMap["RESUMING"] = typeof(Statement.ResumeStatement);
            AbstainMap["STASHING"] = typeof(Statement.StashStatement);
            AbstainMap["RETRIEVING"] = typeof(Statement.RetrieveStatement);
            AbstainMap["IGNORING"] = typeof(Statement.IgnoreStatement);
            AbstainMap["REMEMBERING"] = typeof(Statement.RememberStatement);
            AbstainMap["ABSTAINING"] = typeof(Statement.AbstainStatement);
            AbstainMap["REINSTATING"] = typeof(Statement.ReinstateStatement); ;
            AbstainMap["CALCULATING"] = typeof(Statement.CalculateStatement);
            AbstainMap["COMING FROM"] = typeof(Statement.ComeFromStatement);
            AbstainMap["BOXING"] = typeof(Statement.CalculateStatement); // box creation is a calculate
            AbstainMap["FEEDING"] = typeof(Statement.FeedStatement);
            AbstainMap["PETTING"] = typeof(Statement.PetStatement);
        }

        public CompilationContext()
        {
            this.assemblyType = AssemblyType.exe;
        }

        public override string ToString() { return source.ToString(); }

        public void ReplaceMarker(string marker, string replacement)
        {
            source.Replace(marker, replacement);
        }

        public void Emit(string s)
        {
            source.Append(s);
            source.Append(";\r\n");
        }

        public void EmitRaw(string s)
        {
            source.Append(s);
        }

        public void Warn(string s)
        {
            Console.WriteLine("Warning: " + s);
        }
        
        public string GeneratePropertyName(string className)
        {
            string[] s = className.Split('.');
            return String.Join(null, s) + "Prop";
        }

        internal void EmitRaw(Expression depth)
        {
            throw new NotImplementedException();
        }
    }

    class Compiler
    {
        static string PrepareSource(IEnumerable<string> files)
        {
            //First verify all files exist and have the right extension...
            string src = null;

            foreach (string file in files)
            {
                Trace.WriteLine(string.Format("Processing source file '{0}'", file));
                int dot = file.IndexOf('.');
                if (dot < 0)
                    throw new CompilationException(Messages.E998 + " (" + file + ")");

                string extension = file.Substring(dot);
                if (extension != ".i")
                    throw new CompilationException(Messages.E998 + " (" + file + ")");

                try
                {
                    StreamReader r = new StreamReader(file);
                    // Join continuation lines: if a line starts with whitespace
                    // followed by non-keyword content, it's a continuation of the
                    // previous line (standard INTERCAL allows multi-line statements)
                    string raw = r.ReadToEnd();
                    string[] lines = raw.Split('\n');
                    var joined = new System.Text.StringBuilder();
                    for (int li = 0; li < lines.Length; li++)
                    {
                        string line = lines[li].TrimEnd('\r');
                        if (li > 0 && line.Length > 0 && (line[0] == ' ' || line[0] == '\t'))
                        {
                            // Check if this is a continuation (doesn't start a new statement)
                            string trimmed = line.TrimStart();
                            if (trimmed.Length > 0 && trimmed[0] != '(' &&
                                !trimmed.StartsWith("DO") && !trimmed.StartsWith("PLEASE"))
                            {
                                // Continuation line — append without newline
                                Console.Error.WriteLine("JOINING: " + trimmed);
                                joined.Append(" " + trimmed);
                                continue;
                            }
                        }
                        joined.Append(line + "\n");
                    }
                    src += joined.ToString();
                    r.Close();
                }

                catch (Exception e)
                {
                    Exception err = new CompilationException(Messages.E777 + " (" + file + ")", e);
                    throw err;
                }
            }


            return src;
        }

        static void EmitBinary(CompilationContext c)
        {
            try
            {
                StreamWriter writer = new StreamWriter("~tmp.cs");
                writer.Write(c.ToString());
                writer.Close();
            }
            catch (Exception e)
            {
                throw new CompilationException(Messages.E888, e);
            }

            //Generate a temporary .csproj to compile the emitted C# source.
            //This replaces the old approach of shelling out to csc.exe directly,
            //which doesn't work on modern .NET without extra setup.
            string outputType = "Exe";
            string assemblyFileName = c.assemblyName + ".exe";
            if (c.assemblyType == CompilationContext.AssemblyType.library)
            {
                outputType = "Library";
                assemblyFileName = c.assemblyName + ".dll";
            }

            StringBuilder csproj = new StringBuilder();
            csproj.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
            csproj.AppendLine("  <PropertyGroup>");
            csproj.AppendLine("    <OutputType>" + outputType + "</OutputType>");
            csproj.AppendLine("    <TargetFramework>net9.0</TargetFramework>");
            csproj.AppendLine("    <AssemblyName>" + c.assemblyName + "</AssemblyName>");
            csproj.AppendLine("    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>");
            // Always emit debug symbols for source-level debugging
            csproj.AppendLine("    <DebugType>portable</DebugType>");
            csproj.AppendLine("    <DebugSymbols>true</DebugSymbols>");
            if (c.debugBuild)
            {
                csproj.AppendLine("    <DefineConstants>TRACE</DefineConstants>");
            }
            csproj.AppendLine("  </PropertyGroup>");
            csproj.AppendLine("  <ItemGroup>");
            csproj.AppendLine("    <Compile Include=\"~tmp.cs\" />");
            csproj.AppendLine("  </ItemGroup>");

            //We need to pass references down to the C# compiler
            if (c.references != null)
            {
                csproj.AppendLine("  <ItemGroup>");
                for (int i = 0; i < c.references.Length; i++)
                {
                    string refPath = Path.GetFullPath(c.references[i].assemblyFile);
                    csproj.AppendLine("    <Reference Include=\"" + Path.GetFileNameWithoutExtension(refPath) + "\">");
                    csproj.AppendLine("      <HintPath>" + refPath + "</HintPath>");
                    csproj.AppendLine("    </Reference>");
                }
                csproj.AppendLine("  </ItemGroup>");
            }

            csproj.AppendLine("</Project>");

            try
            {
                File.WriteAllText("~tmp.csproj", csproj.ToString());
            }
            catch (Exception e)
            {
                throw new CompilationException(Messages.E888, e);
            }

            string compiler = "dotnet";
            string userSpecifiedCompilerPath = Environment.GetEnvironmentVariable("INTERCAL_DOTNET_PATH");
            if (!string.IsNullOrEmpty(userSpecifiedCompilerPath))
            {
                compiler = userSpecifiedCompilerPath;
            }

            string configuration = c.debugBuild ? "Debug" : "Release";
            string compiler_args = "build ~tmp.csproj -c " + configuration +
                " -o " + '"' + Environment.CurrentDirectory + '"';

            try
            {
                Trace.WriteLine(string.Format("{0} {1}", compiler, compiler_args));

                ProcessStartInfo si = new ProcessStartInfo(compiler, compiler_args);
                si.UseShellExecute = false;
                si.CreateNoWindow = true;
                Process p = Process.Start(si);
                p.WaitForExit();

                if(p.ExitCode == 0)
                {
                    CopyRequiredBinariesToOutputFolder(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Abort(Messages.E2003);
            }

            //File.Delete("~tmp.cs");
            //File.Delete("~tmp.csproj");
        }

        private static void CopyRequiredBinariesToOutputFolder(CompilationContext c)
        {
            Trace.WriteLine("Copying binaries to output folder...");
            foreach(var reference in c.references)
            {
                //note that we will skip files in the GAC
               if(File.Exists(reference.assemblyFile))
                {
                    var sourceFileName = Path.GetFullPath(reference.assemblyFile);
                    var destFileName = Path.Combine(Environment.CurrentDirectory, Path.GetFileName(sourceFileName));
                    Trace.WriteLine(string.Format("Copying '{0}' to '{1}'", sourceFileName, destFileName));

                    if(sourceFileName != destFileName)
                        File.Copy(sourceFileName, destFileName, true);
                }
               else
                {
                    Trace.WriteLine(string.Format("Not copying '{0}' (File is in the GAC?)", reference.assemblyFile));
                }
            }
        }

        const string Banner =
            "Simple INTERCAL Compiler version {0}\r\n" +
            "for Microsoft (R) .NET version {1}\r\n" +
            "Authorship disclaimed by Jason Whittington 2017. All rights reserved.\r\n\r\n";
        const string Usage =
        #region usage
            "                        SICK Compiler Options\r\n" +

            "                        - OUTPUT FILES -\r\n" +
            "/t:exe                  Build a console executable (default)\r\n" +
            "/t:library              Build a library \r\n" +

            "\r\n                      - INPUT FILES -\r\n" +
            "/r:<file list>          Reference metadata from the specified assembly files\r\n" +

            "\r\n                      - CODE GENERATION -\r\n" +
            "/debug+                 Emit debugging information\r\n" +
            "/base:<class_name>      Use specified class as base class (e.g. MarshalByRefObject)\r\n" +
            "/public:<label_list>    Only emit stubs for the specified labels (ignored for .exe builds)\r\n" +

            "\r\n                      - ERRORS AND WARNINGS -\r\n" +
            "/b                      Reduce probably of E774 to zero.\r\n" +
            "/v or /verbose          Verbose compiler output\r\n" +
            "/noplease              Skip politeness checking\r\n";
        #endregion

        const int MinimumPoliteness = 20;
        const int MaximumPoliteness = 34;

        static void Main(string[] args)
        {

            Console.WriteLine(Banner,
                Assembly.GetExecutingAssembly().GetName().Version,
                Environment.Version);

            Trace.Listeners.Clear();

            try
            {

                if (args.Length == 0)
                {
                    Abort(Messages.E777);
                    return;
                }

                else if (args.Length == 1 && args[0].IndexOf("?") >= 0)
                {
                    Console.WriteLine(Usage);
                    return;
                }

                //Parse arguments...
                CompilationContext c = new CompilationContext();
                List<string> sources = new List<string>();

                foreach (string arg in args)
                {
                    if ((arg[0] == '-') || (arg[0] == '/'))
                    {
                        if (arg.Substring(1).ToLower() == "v" ||
                            arg.Substring(1).ToLower() == "verbose")
                            Trace.Listeners.Add(new ConsoleTraceListener());

                        else if (arg.IndexOf("t:") == 1)
                            switch (arg.Substring(3))
                            {
                                case "library": c.assemblyType = CompilationContext.AssemblyType.library; break;
                                case "exe": c.assemblyType = CompilationContext.AssemblyType.exe; break;
                                default: Abort(Messages.E2001); break;
                            }

                        //using /r lets a programmer reference labels in another library, which allows DO NEXT
                        //to implicitly make calls into another component. 
                        else if (arg.IndexOf("r:") == 1)
                        {
                            string[] refs = (arg.Substring(3)).Split(',');
                            c.references = new ExportList[refs.Length + 1];

                            //For every referenced assembly we need to go drag out the labels exported
                            //by that assembly and store them on the context. NextStatement will use this 
                            //information to generate calls to the library.  In the case of duplicate labels
                            //behavior is undefined, chances are the first library listed with a matching label
                            //will be the one used.
                            for (int i = 0; i < refs.Length; i++)
                            {
                                Trace.WriteLine(string.Format("Referencing '{0}'", refs[i]));
                                c.references[i] = new ExportList(refs[i]);
                            }

                            //We put syslib in last. If other libs define labels that collide with
                            //syslibs then those will get precedence over the standard ones.
                            c.references[refs.Length] = new ExportList(FindFile("intercal.runtime.dll"));
                        }
                        else if (arg.IndexOf("DEBUG+") > 0 || arg.IndexOf("debug+") > 0)
                        {
                            Trace.WriteLine("Emitting a Debug build");
                            c.debugBuild = true;
                        }

                        //this option can be used to control which labels to make public.  If it
                        //is left off then all labels are made public.  This option only makes sense
                        //when used with the /t:library option.  It is ignored for .EXE builds.
                        else if (arg.IndexOf("public:") == 1)
                        {
                            c.publicLabels = new Dictionary<string, bool>();
                            string[] labels = (arg.Substring(8)).Split(',');
                            foreach (string s in labels)
                                c.publicLabels[s] = true;
                        }

                        //Let the user specify the base class.  For example, setting the base
                        //class to System.Web.UI.Page allows the resulting assembly to be used
                        //as a codebehind assembly.
                        else if (arg.IndexOf("base:") == 1)
                        {
                            c.baseClass = arg.Substring(6);
                            Trace.WriteLine(string.Format("Setting base type to {0}", c.baseClass));
                        }

                        // /b reduces the probability of E774 to zero.
                        else if (arg.IndexOf("b") == 1)
                        {
                            Trace.WriteLine("(Intentional) Bugs disabled");
                            c.Buggy = false;
                        }

                        else if (arg.Substring(1).ToLower() == "noplease")
                        {
                            Trace.WriteLine("Politeness checking disabled");
                            c.skipPoliteness = true;
                        }
                    }

                    else
                    {
                        sources.Add(arg);
                    }
                }

                //Auto-include standard lib if it hasn't been referenced already
                if (c.references == null)
                {
                    var refs = new List<ExportList>();
                    var syslibPath = TryFindFile("isyslib.dll");
                    // Don't self-reference when compiling the syslib itself
                    if (syslibPath != null && Path.GetFileNameWithoutExtension(sources[0]) != "isyslib")
                    {
                        Trace.WriteLine("Auto-referencing isyslib.dll");
                        refs.Add(new ExportList(syslibPath));
                    }
                    refs.Add(new ExportList(FindFile("intercal.runtime.dll")));
                    c.references = refs.ToArray();
                }


                //do the compilation
                string src = PrepareSource(sources);
                StreamWriter fs = new StreamWriter("~tmp.i");
                fs.Write(src);
                fs.Close();

                //Creating a program object parses it - any compile time errors will 
                //show up as an exception here. If we do get an exception we purposely
                //leave ~tmp.i sitting on the hard drive for the programer to inspect
                Trace.WriteLine("Parsing...");
                Program p = Program.CreateFromFile("~tmp.i");

                //Now do politeness checking.  No point until we have
                //at least three statements in the program.
                //
                //Note that componentization affects politeness: a program that is
                //polite as a whole may fail when broken into components, since this
                //compiler enforces politeness per component.  Use /noplease to skip.
                Trace.WriteLine("Analyzing Politeness...");
                if (!c.skipPoliteness && p.StatementCount > 3)
                {
                    //less than 1/5 politeness level is not polite enough
                    if (p.Politeness < MinimumPoliteness)
                    {
                        Abort(Messages.E079);
                    }
                    //more than 1/3 and you are too polite
                    else if (p.Politeness > MaximumPoliteness)
                    {
                        Abort(Messages.E099);
                    }
                }


                c.program = p;
                c.assemblyName = Path.GetFileNameWithoutExtension(sources[0]);
                c.sourceFile = Path.GetFullPath(sources[0]);

                // Check for label conflicts between local program and referenced assemblies
                if (c.references != null)
                {
                    foreach (var e in c.references)
                    {
                        foreach (var a in e.entryPoints)
                        {
                            if (p[a.Label].GetEnumerator().MoveNext())
                            {
                                Abort(string.Format("E2005 LABEL {0} IS DEFINED LOCALLY AND ALSO EXPORTED BY {1}.  AMBIGUITY IS NOT POLITE",
                                    a.Label, Path.GetFileName(e.assemblyFile)));
                            }
                        }
                    }
                }

                Trace.WriteLine("Emitting C#...");
                p.EmitCSharp(c);

                File.Delete("~tmp.i");

                Trace.WriteLine("Emitting Binaries...");
                EmitBinary(c);
            }

            catch (Exception e)
            {
                Abort(e.Message);
            }

        }

        private static string TryFindFile(string path)
        {
            if (File.Exists(path)) return path;
            var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var srcPath = Path.Combine(baseDir, path);
            if (File.Exists(srcPath)) return srcPath;
            return null;
        }

        private static string FindFile(string path)
        {
            if (File.Exists(path))
            {
                return path;
            }
            else
            {
                var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var srcPath = Path.Combine(baseDir, path);
                if (File.Exists(srcPath))
                {
                    return  srcPath;
                }
            }

            throw new IntercalException(Messages.E2002);
        }

        static void Abort(string error)
        {
            Console.WriteLine(error);
            Console.WriteLine("     CORRECT SOURCE AND RESUBMIT");
        }
    }
}