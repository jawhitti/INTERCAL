using System;
using INTERCAL.Runtime;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;

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
        public AssemblyType assemblyType;
        public bool debugBuild = false;

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
        }

        public CompilationContext()
        {
            this.assemblyType = AssemblyType.exe;
        }

        public override string ToString() { return source.ToString(); }

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
                int dot = file.IndexOf('.');
                if (dot < 0)
                    throw new CompilationException(Messages.E998 + " (" + file + ")");

                string extension = file.Substring(dot);
                if (extension != ".i")
                    throw new CompilationException(Messages.E998 + " (" + file + ")");

                try
                {
                    StreamReader r = new StreamReader(file);
                    src += r.ReadToEnd();
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
            StreamWriter writer = new StreamWriter("~tmp.cs");
            writer.Write(c.ToString());
            writer.Close();

            string compiler = "csc.exe";
            string userSpecifiedCompilerPath = ConfigurationManager.AppSettings["compilerPath"];
            if (!string.IsNullOrEmpty(userSpecifiedCompilerPath)) {
                compiler = userSpecifiedCompilerPath; 
            }
 
            string compiler_args = null;

            if (c.debugBuild)
            {
                compiler_args = "/debug+ /d:TRACE ";
            }

            switch (c.assemblyType)
            {
                case CompilationContext.AssemblyType.exe:
                    compiler_args += " /out:" + c.assemblyName + ".exe ";
                    break;

                case CompilationContext.AssemblyType.library:
                    compiler_args += " /t:library /out:" + c.assemblyName + ".dll ";
                    break;
            }

            bool needsComma = false;

            compiler_args += (" /r:");

            //We need to pass references down to the C# compiler
            if (c.references != null)
            {
                for (int i = 0; i < c.references.Length; i++)
                {
                    if (needsComma)
                        compiler_args += ",";

                    compiler_args += c.references[i].assemblyFile;
                    needsComma = true;
                }
            }
            compiler_args += " ~tmp.cs";

            try
            {
                //Uncomment this to display the command used to invoke the compiler
                Console.WriteLine("{0} {1}", compiler, compiler_args);

                ProcessStartInfo si = new ProcessStartInfo(compiler, compiler_args);
                si.UseShellExecute = false;
                si.CreateNoWindow = true;
                Process p = Process.Start(si);
                p.WaitForExit();
            }
            catch (Exception e)
            {
                Abort(Messages.E2003);
            }

            //File.Delete("~tmp.cs");
        }

        const string Banner =
            "Simple INTERCAL Compiler version {0}\n" +
            "for Microsoft (R) .NET Framework version {1}\n" +
            "Authorship disclaimed by Jason Whittington 2003. All rights reserved.\n\n";
        const string Usage =
        #region usage
            "                        SICK Compiler Options\n" +

            "                        - OUTPUT FILES -\n" +
            "/t:exe                  Build a console executable (default)\n" +
            "/t:library              Build a library \n" +

            "\n                      - INPUT FILES -\n" +
            "/r:<file list>          Reference metadata from the specified assembly files\n" +

            "\n                      - CODE GENERATION -\n" +
            "/debug+                 Emit debugging information\n" +
            "/base:<class_name>      Use specified class as base class (e.g. MarshalByRefObject)\n" +
            "/public:<label_list>    Only emit stubs for the specified labels (ignored for .exe builds)\n" +

            "\n                      - ERRORS AND WARNINGS -\n" +
            "/b                      Reduce probably of E774 to zero.\n";
        #endregion

        const int MinimumPoliteness = 20;
        const int MaximumPoliteness = 34;

        static void Main(string[] args)
        {

            Console.WriteLine(Banner,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version,
                System.Environment.Version);
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
                        if (arg.IndexOf("t:") == 1)
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
                                c.references[i] = new ExportList(refs[i]);
                            }

                            //We put syslib in last. If other libs define labels that collide with
                            //syslibs then those will get precedence over the standard ones.

                            c.references[refs.Length] = new ExportList("intercal.runtime.dll");
                        }
                        else if (arg.IndexOf("DEBUG+") > 0 || arg.IndexOf("debug+") > 0)
                        {
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
                        }

                        // /b reduces the probability of E774 to zero.
                        else if (arg.IndexOf("b") == 1)
                        {
                            c.Buggy = false;
                        }
                    }

                    else
                    {
                        sources.Add(arg);
                    }
                }

                //Auto-include standard lib
                if (c.references == null)
                {
                    c.references = new ExportList[1];
                    c.references[0] = new ExportList("intercal.runtime.dll");
                }


                //do the compilation
                string src = PrepareSource(sources);
                StreamWriter fs = new StreamWriter("~tmp.i");
                fs.Write(src);
                fs.Close();

                //Creating a program object parses it - any compile time errors will 
                //show up as an exception here. If we do get an exception we purposely
                //leave ~tmp.i sitting on the hard drive for the programer to inspect
                Program p = Program.CreateFromFile("~tmp.i");

                //Now do politeness checking.  No point until we have 
                //at least three statements in the program.
                if (p.StatementCount > 3)
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
                c.assemblyName = ((string)sources[0]).Substring(0, ((string)sources[0]).IndexOf('.'));
                p.EmitCSharp(c);

                File.Delete("~tmp.i");

                EmitBinary(c);
            }

            catch (Exception e)
            {
                Abort(e.Message);
            }

        }
        static void Abort(string error)
        {
            Console.WriteLine(error);
            Console.WriteLine("     CORRECT SOURCE AND RESUBMIT");
        }
    }
}