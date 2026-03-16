using System;
using INTERCAL.Runtime;
using System.IO;
using System.Text;
using System.Reflection;
using System.Diagnostics;

using System.Collections.Generic;
using System.Linq;

// Version is now set in the .csproj file

namespace INTERCAL
{


    public class CompilationException : Exception
    {
        public CompilationException() { }
        public CompilationException(string message) : base(message) { }
        public CompilationException(string message, Exception inner) : base(message, inner) { }
    }

    //This is the worlds lamest input scanner
    public class Scanner
    {
        public int LineNumber = 1;
        Tokenizer tokenizer;
        Token current;
        Token next;
        int newlines;

        public Scanner(Tokenizer t)
        {
            tokenizer = t;
            current = tokenizer.NextToken();

            while (current.Type == TokenType.Newline)
            {
                newlines++;
                current = tokenizer.NextToken();
            }

            next = tokenizer.NextToken();
        }

        public Token Current
        {
            get { return current; }
        }

        public Token PeekNext
        {
            get { return next; }
        }

        public void MoveNext()
        {
            //The only complication here is that
            //we swallow "\n" internally so the
            //expression parsers never see it.
            current = next;
            LineNumber += newlines;
            newlines = 0;

            next = tokenizer.NextToken();

            while (next.Type == TokenType.Newline)
            {
                newlines++;
                next = tokenizer.NextToken();
            }
        }

        public void Panic()
        {
            //We basically start dropping tokens until we find either a "DO/PLEASE DO" or
            //a label followed by a DO/PLEASE DO.
            for (;;)
            {
                MoveNext();
                if (current.Type == TokenType.EOF)
                    break;
                if (current.Type == TokenType.Prefix)
                    break;
                else if ((current.Type == TokenType.Label) && (next.Type == TokenType.Prefix))
                    break;
            }
        }

        public static Scanner CreateScanner(string input)
        {
            return new Scanner(new Tokenizer(input));
        }

        private static readonly Dictionary<string, TokenType> GroupTypeMap = new Dictionary<string, TokenType>
        {
            { "label", TokenType.Label },
            { "digits", TokenType.Digits },
            { "prefix", TokenType.Prefix },
            { "gerund", TokenType.Gerund },
            { "statement", TokenType.Statement },
            { "separator", TokenType.Separator },
            { "var", TokenType.Var },
            { "unary_op", TokenType.UnaryOp },
            { "binary_op", TokenType.BinaryOp },
        };

        public string ReadGroupValue(string group)
        {
            if (GroupTypeMap.TryGetValue(group, out TokenType expected) && Current.Type == expected)
                return Current.Value;
            else
                throw new ParseException(String.Format("line {0}: '{2}' is not a valid {1}", LineNumber, group, Current.Value));
        }

        public void VerifyToken(string val)
        {
            if (Current.Value != val)
                throw new ParseException(String.Format("line {0}: Expected a {1}", LineNumber, val));
        }

    }



    public class Program
    {
        //A Program maintains a List of statements. 
        public List<Statement> Statements = new List<Statement>();
        public IEnumerable<Statement> OccurencesOf(Type t)
        {
            return from s in Statements
                   where s.GetType() == t
                   select s;
        }

        //We only need to include the generic abstain guard for statements that 
        //are actually targets of an abstain.  Whenever we see an abstain statement
        //or a disabled ("NOT") statement we put an entry in one of these
        //two structures so the generator can emit the abstain guard.
        Dictionary<Type, bool> AbstainedGerunds = new Dictionary<Type, bool>();

        //statements holds indices
        Dictionary<string, bool> AbstainedLabels = new Dictionary<string, bool>();

        //allow the outside world to enumerate statements.
        //public IEnumerator GetEnumerator()
        //{
        //	return Statements.GetEnumerator();
        //}
        public int StatementCount
        {
            get
            {
                return Statements.Count;
            }
        }
        public IEnumerable<Statement> this[string label]
        {
            get
            {
                return Statements.Where(s => s.Label == label);
            }
        }
        public Statement this[int n]
        {
            get { return Statements[n]; }
        }


        //TODO: This will need to deal with external calls. A "SafeRecursion"
        //attribute or something would let the runtime query to see if calls out need
        //to be done on a dedicated thread or if the call can be made directly.
        bool IsSimpleFlow(int i, Stack<int> statementsExamined)
        {
            try
            {
                //if we find a cycle just bail out cause that's bad juju
                if (statementsExamined.Contains(i))
                {
                    statementsExamined.ToList().ForEach(n => Console.WriteLine(n));
                    Console.WriteLine("Cycle detected encountered at line {0}", Statements[i].LineNumber);

                    return false;
                }

                statementsExamined.Push(i);

                //If trapdoor is true then follow the COME FROM
                //If the statement has a % modifier then we need 
                //to ALSO ensure the successor is safe
                if (Statements[i].Trapdoor > 0)
                {
                    var safeTarget = IsSimpleFlow(Statements[i].Trapdoor, statementsExamined);

                    if(safeTarget && Statements[i].Percent == 100)
                    {
                        return true;
                    }
                    else if(safeTarget)
                    {
                        int successor = i + 1;
                        return IsSimpleFlow(successor, statementsExamined);
                    }
                    else
                    {
                        return false;
                    }
                }

                else if (Statements[i] is Statement.ResumeStatement)
                {
                    if (Statements[i].Percent < 100)
                    {
                        int successor = i + 1;
                        return IsSimpleFlow(successor, statementsExamined);
                    }
                    else
                    {
                        statementsExamined.ToList().ForEach(n => Console.WriteLine(n));
                        Console.WriteLine("RESUME encountered at line {0}", Statements[i].LineNumber);

                        //OOPS.  This isn't actually enough.  We need to handle depth.  
                        //And from what I've seen so far INTERCAL programs by and large
                        //wind up not being safe, so this optimization is likely 
                        //not as impactful as I thought. 
                        return true;
                    }
                }
                else if (Statements[i] is Statement.GiveUpStatement)
                {
                    if (Statements[i].Percent < 100)
                    {
                        int successor = i + 1;
                        return IsSimpleFlow(successor, statementsExamined);
                    }
                    else
                    {
                        statementsExamined.ToList().ForEach(n => Console.WriteLine(n));
                        Console.WriteLine("GIVE UP encountered at line {0}", Statements[i].LineNumber);
                        return true;
                    }
                }

                else if (Statements[i] is Statement.ForgetStatement)
                {
                    //FORGET is ALWAYS false. Even if it has a percentage
                    //attached to it it still introduces the possibility of
                    //a forget.
                    statementsExamined.ToList().ForEach(n => Console.WriteLine(n));
                    Console.WriteLine("FORGET encountered at line {0}", Statements[i].LineNumber);
                    return false;
                }
                else if (Statements[i] is Statement.NextStatement)
                {
                    //A NEXT statement is safe if:
                    // a) the flow beginning at the target is safe
                    // b) the flow of the successor is safe.

                    Statement.NextStatement ns = Statements[i] as Statement.NextStatement;
                    var target = Statements.Where(s => s.Label == ns.Target).FirstOrDefault();
                    if(target == null)
                    {

                        statementsExamined.ToList().ForEach(n => Console.WriteLine(n));
                        Console.WriteLine("External call encountered at line {0}", Statements[i].LineNumber);
                        return false;
                    }

                    var isTargetSafe =  IsSimpleFlow(target.StatementNumber, statementsExamined);

                    if (!isTargetSafe)
                        return false;
                    else
                    {
                        int successor = i + 1;
                        return IsSimpleFlow(successor, statementsExamined);
                    }
                }
                else
                {
                    int successor = i + 1;
                    return IsSimpleFlow(successor, statementsExamined);
                }
            }
            finally
            {
                statementsExamined.Pop();
            }
        }

        Program(string input)
        {
            ParseStatements(input);

            FixupComeFroms();

            //Any stray labels will need to get liked up with external libraries somehow.  For 
            //example, if the program says "DO 1020 NEXT" and is compiling against intercal.runtime.dll
            //then we want to emit an external call to 1020.  We don't do anything about it now, but 
            //we will in NextStatement.Emit
        }

        #region frontend
        //This function is called after all the statements are parsed. It walks the list
        //of statements and links up any COME FROM statements
        void FixupComeFroms()
        {
            for (int i = 0; i < Statements.Count; i++)
            {
                Statement.ComeFromStatement s = this[i] as Statement.ComeFromStatement;

                if (s != null)
                {
                    string Target = s.Target;
                    Statement target = this[Target].First();

                    if (target == null)
                        throw new CompilationException(Messages.E444 + s.Target);

                    if (target.Trapdoor < 0)
                        target.Trapdoor = i;
                    else
                        throw new CompilationException(Messages.E555 + s.Target);
                }
            }
        }


        void ParseStatements(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                if (c != '!')
                    sb.Append(c);
                else
                {
                    //replace ! with '.
                    sb.Append("'");
                    sb.Append('.');
                }
            }

            string src = sb.ToString();
            Scanner scanner = Scanner.CreateScanner(src.ToString());
            int begin = scanner.Current.Index;
            int end = begin;

            while (scanner.Current.Type != TokenType.EOF)
            {
                //begin and end are used to snip out a substring for
                //each statement.
                begin = end;
                Statement s = Statement.CreateStatement(scanner);
                end = scanner.Current.Index;
                if (end > begin)
                    s.StatementText = src.Substring(begin, end - begin).TrimEnd();
                else if (scanner.Current.Type == TokenType.EOF)
                    s.StatementText = src.Substring(begin).TrimEnd();


                s.StatementNumber = StatementCount;
                Statements.Add(s);

                //If this statement abstains a gerund then add the 
                //entry to AbstainedGerunds.  If this statement abstains
                //a label then add an entry to AbstainedLabels.
                Statement.AbstainStatement a = s as Statement.AbstainStatement;

                if (a != null)
                {
                    if (a.Target != null)
                        this.AbstainedLabels[a.Target] = true;
                    else if (a.Gerunds != null)
                    {
                        foreach (string gerund in a.Gerunds)
                        {
                            Type t = (Type)CompilationContext.AbstainMap[gerund];
                            Debug.Assert(t != null);

                            this.AbstainedGerunds[t] = true;
                        }
                    }
                }

                //Debug.Assert(this.OccurencesOf[s.GetType()] != null) ;
                //this.OccurencesOf[s.GetType()] 
                //	= (int)this.OccurencesOf[s.GetType()] + 1;


                //2016 TODO -- how do we deal with this when we switch
                //OccurencesOf over to a selector instead of a collection? 

                //This was a bug discovered Jan 28, 2003. If a statement is disabled
                //by default then we treat it as an additional abstain.
                //if (!s.bEnabled)
                //	this.OccurencesOf[typeof(Statement.AbstainStatement)]
                //	= (int)this.OccurencesOf[typeof(Statement.AbstainStatement)] + 1;

                if (s.Label != null)
                {
                    //Check for duplicate label
                    if (this[s.Label].Count() > 1)
                        throw new CompilationException(Messages.E182 + " " + s.Label);
                }
            }
        }

        public int Politeness
        {
            get
            {
                int please = 0;
                int statements = 0;

                foreach (Statement s in Statements)
                {
                    if (s as Statement.SentinelStatement == null)
                    {
                        statements++;

                        if (s.bPlease)
                            please++;
                    }
                }

                return (int)(((double)please / (double)statements) * 100.0);
            }
        }
        #endregion

        #region backend
        public void EmitAttributes(CompilationContext ctx)
        {
            //We emit attributes in the metadata to advertise which programmatic
            //labels this assembly exports.  Future versions of the compiler should
            //have a /hide:<labels> (or something) command to suppress exposing
            //nonpublic lables.

            //Note that we do NOT emit an attribute that can be used to just run
            //a program compiled up.  
            ctx.EmitRaw("\r\n//These attributes are used by the compiler for component linking\r\n");

            foreach (Statement s in Statements)
            {
                if (s.Label != null)
                {
                    if ((ctx.publicLabels != null) &&
                        (ctx.publicLabels[s.Label] == false))
                    {
                        continue;
                    }


                    ctx.EmitRaw("[assembly: EntryPoint(\"" + s.Label + "\", \"" + ctx.assemblyName + "\", \"DO_" + s.Label.Substring(1, s.Label.Length - 2) + "\")]\r\n");
                }

            }
            ctx.EmitRaw("\n\n");

        }

        public void EmitEntryStubs(CompilationContext ctx)
        {
            //We emit a helpful stub for every label (unless the user has suppressed
            //some of them by using "/public:"
            foreach (Statement s in Statements)
            {
                if (s.Label != null)
                {
                    if ((ctx.publicLabels != null) &&
                       (ctx.publicLabels[s.Label] == false))
                    {
                        continue;
                    }


                    ctx.EmitRaw("public void DO_" + s.Label.Substring(1, s.Label.Length - 2) + "(ComponentCall call)\r\n{\r\n");
                    ctx.EmitRaw("   var frame = new ExecutionFrame(call.Context, Eval, " + s.Label.Substring(1, s.Label.Length - 2) + "L, call);\r\n");
                    ctx.EmitRaw("   Eval(frame);\r\n");
                    ctx.EmitRaw("}\r\n\r\n");
                }
            }

            //This is the "late bound" version that allows clients to dynamically pass a label. *ALL* labels
            //can be accessed this way.
            if (ctx.assemblyType == CompilationContext.AssemblyType.library)
            {
                ctx.EmitRaw("   public void DO(ExecutionContext context, string label)\r\n   {\r\n");
                ctx.EmitRaw("      switch(label)\r\n");
                ctx.EmitRaw("      {\r\n");

                foreach (Statement s in Statements)
                {
                    if (s.Label != null)
                    {
                        if ((ctx.publicLabels != null) &&
                            (ctx.publicLabels[s.Label] == false))
                        {
                            continue;
                        }

                        long labelValue = long.Parse(s.Label.Substring(1, s.Label.Length - 2));

                        ctx.EmitRaw("         case \"" + s.Label + "\": ");
                        ctx.EmitRaw("context.Evaluate(Eval," + labelValue + "L);  ");
                        ctx.EmitRaw("break;\r\n");
                    }

                }

                ctx.EmitRaw("      }\r\n   }\r\n\r\n");
            }
        }

        public void EmitAbstainMap(CompilationContext ctx)
        {
            int abstains = OccurencesOf(typeof(Statement.AbstainStatement)).Count();

            abstains += Statements.Where(s => !s.bEnabled || s.Splatted).Count();

            if (abstains > 0)
            {
                //This array holds one entry for every statement that might 
                //be abstained, representing an improvement over C-INTERCAL.
                //The following code is a mess and could certainly be 
                //refactored.
                ctx.EmitRaw("   bool[] abstainMap = new bool[] {");

                int slot = 0;
                bool bfirst = true;
                for (int i = 0; i < Statements.Count; i++)
                {
                    Statement s = (Statement)Statements[i];

                    //Does this statement need an abstain guard?
                    //In the goto model, splatted statements also get guards (starting
                    //disabled) to prevent fall-through from abstained predecessors.
                    if ((!s.bEnabled) || s.Splatted ||
                        (this.AbstainedGerunds.ContainsKey(s.GetType())) ||
                        (s.Label != null && this.AbstainedLabels.ContainsKey(s.Label)))
                    {
                        if (!bfirst)
                        {
                            ctx.EmitRaw(",");
                        }
                        else
                        {
                            bfirst = false;
                        }

                        ctx.EmitRaw((s.bEnabled && !s.Splatted) ? "true" : "false");
                        ((Statement)Statements[i]).AbstainSlot = slot;
                        slot++;
                    }
                }

                ctx.EmitRaw("};\n\n");
            }

            // NEXT stack for goto-based state machine (field, not local, to survive gotos)
            ctx.EmitRaw("   System.Collections.Generic.Stack<int> _nextStack = new System.Collections.Generic.Stack<int>();\r\n");
            ctx.EmitRaw("   int _forgetAdj = 0;\r\n\r\n");
        }

        public void EmitDispatchMap(CompilationContext ctx)
        {
            //If we don't have any labels then we don't need 
            //to emit this switch block.

            var labels = from s in Statements
                         where !string.IsNullOrEmpty(s.Label)
                         select s.Label;

            if (labels.Count() > 0)
            {
                //ctx.EmitRaw("dispatch:\n");
                ctx.EmitRaw("   switch(frame.Label)\r\n   {\r\n");

                foreach (Statement s in Statements)
                {
                    if (s.Label != null)
                    {
                        long labelNum = long.Parse(s.Label.Substring(1, s.Label.Length - 2));
                        ctx.EmitRaw("      case " + labelNum + "L: ");
                        // Push sentinel so RESUME can pop the "caller" level
                        ctx.EmitRaw("_nextStack.Push(0); ");
                        ctx.EmitRaw("goto label_" + labelNum + ";\r\n");
                    }
                }

                ctx.EmitRaw("   }\r\n");
            }
        }

        public void EmitProgramProlog(CompilationContext ctx)
        {
            ctx.Emit("using System");
            //ctx.Emit("using System.Threading");
            ctx.Emit("using INTERCAL.Runtime");
            ctx.Emit("using System.Diagnostics");

            if (ctx.assemblyType == CompilationContext.AssemblyType.library)
                this.EmitAttributes(ctx);

            //There's nothing on this clas that can't be serialized, I don't think
            ctx.EmitRaw("[Serializable]\n");
            ctx.EmitRaw("public class " + ctx.assemblyName + " : " + ctx.baseClass + "\n{ \n");

            ctx.EmitRaw(
                "   public void Run(){\r\n" +
                "      ExecutionContext ec = INTERCAL.Runtime.ExecutionContext.CreateExecutionContext();\r\n" +
                "      ec.Run(Eval);\r\n" +
                "   }\r\n\r\n");

            //We assume that EXEs do not want to expose labels and that DLLs do.
            if (ctx.assemblyType == CompilationContext.AssemblyType.library)
                EmitEntryStubs(ctx);

            this.EmitAbstainMap(ctx);

            //Now we emit the main function.  Eval overrides the virtual function from the base class.
            ctx.EmitRaw("   protected void Eval(ExecutionFrame frame)" +
                        "   {\r\n");

            this.EmitDispatchMap(ctx);

            // _nextStack is a field on the class, declared in EmitAbstainMap

            // Placeholder for debug locals — replaced after all statements are emitted
            ctx.EmitRaw("/*DEBUG_LOCALS_PLACEHOLDER*/\r\n");

        }

        public void EmitProgramEpilog(CompilationContext ctx)
        {
            ctx.EmitRaw(
            "      //Generic catch-all if the program\r\n" +
            "      throw new Exception(Messages.E633);\r\n\r\n" +
            "   exit:\r\n" +
            "      return;\r\n" +
            "   }\r\n\r\n");

            this.EmitProperties(ctx);
            ctx.EmitRaw("}\r\n\r\n");

            if (ctx.assemblyType == CompilationContext.AssemblyType.exe)
            {
                string configFileName = ctx.assemblyName + ".exe.config";
                //This enables remoting, such as it is.
                ctx.EmitRaw("class entry\r\n{\n");
                ctx.EmitRaw("   static void Main(string[] args)\r\n{\r\n");

                //ctx.EmitRaw("if(System.IO.File.Exists(\"" + configFileName + "\"))\n");
                //ctx.EmitRaw("      if(args.Length >= 1 && args[0].IndexOf(\"/config:\") == 0)");
                //ctx.EmitRaw("         System.Runtime.Remoting.RemotingConfiguration.Configure(\"" + configFileName +  "\");\n\n");

                //Uncomment these three lines if you want the program to pause when it
                //starts up.  This is useful if you want to attach a debugger before
                //the program runs.
                //ctx.EmitRaw("      Console.WriteLine(\"press Enter to run:\");\r\n");
                //ctx.EmitRaw("      Console.ReadLine();\r\n");
                //ctx.EmitRaw("      while(Console.In.Peek() != -1) { Console.Read(); }\r\n\r\n");

                ctx.EmitRaw("      //Speed up startup time by ensuring adequate thread availability\r\n");
                ctx.EmitRaw("      System.Threading.ThreadPool.SetMinThreads(80, 4);\r\n\r\n");

                ctx.EmitRaw(
                 "      try\r\n" +
                 "      {\r\n");
                ctx.EmitRaw(string.Format(
                 "         var program = new {0}();\r\n", ctx.assemblyName));
                ctx.EmitRaw(
                 "         program.Run();\r\n" +
                 "      }\r\n");

                if (ctx.debugBuild)
                {
                    ctx.EmitRaw(
                 "      catch (Exception e)\r\n" +
                 "      {\r\n" +
                 "         Console.WriteLine(e);\r\n" +
                 "      }\r\n"
                    );
                }
                else
                {
                    ctx.EmitRaw(
                  "      catch (Exception e)\r\n" +
                  "      {\r\n" +
                  "         Console.WriteLine(e.Message);\r\n" +
                  "      }\r\n"
                     );

                }


                ctx.EmitRaw("   }\r\n");
                ctx.EmitRaw("}\r\n");
            }
        }

        public void EmitProperties(CompilationContext c)
        {
            foreach (string s in c.ExternalReferences)
            {
                string fieldName = "m_" + c.GeneratePropertyName(s);
                c.EmitRaw("\n");
                c.EmitRaw(s + " " + fieldName + ";\n");
                c.EmitRaw(s + " " + c.GeneratePropertyName(s));
                c.EmitRaw("\n{\n");
                c.EmitRaw("   get {");
                c.EmitRaw("if(" + fieldName + "== null) " + fieldName + " = new " + s + "();");
                c.EmitRaw(" return " + fieldName + ";");
                c.EmitRaw("}");
                c.EmitRaw("\n}\n");
            }
        }

        public void EmitStatementProlog(Statement s, CompilationContext c)
        {
            //if the statement has a label then use its label otherwise
            //we just use "line_<line_number>" 
            //Debug.Assert(s.Label != "(2004)");

            //TODO: convert newlines to spaces otherwise multiline statements
            //will 
            c.EmitRaw("\r\n/* ");
            c.EmitRaw(s.StatementText);
            c.EmitRaw("*/\r\n");

            // Emit #line hidden for label/boilerplate, then #line N for the statement
            c.EmitRaw("#line hidden\r\n");

            if (s.Label != null)
            {
                c.EmitRaw("\r\nlabel_" + s.Label.Substring(1, s.Label.Length - 2) + ": \r\n");
            }

            //We need to emit labels for COME FROM so the trapdoor has something to point to.
            else if (s as Statement.ComeFromStatement != null)
            {
                c.EmitRaw("\r\nline_" + s.StatementNumber.ToString() + ":\r\n");
            }
            //Uncomment these lines to emit labels for every single statement.  This
            //is not currently necessary..
            //else
            //	c.EmitRaw("\nline_" + s.StatementNumber.ToString() + ":\n");

            //Now we implement E774: RANDOM COMPILER BUG.  The probability of the
            //bug is 1/256 per statement, which is half that of C-Intercal.  See?
            //this compiler is twice as good!
            if (c.Buggy && c.random.Next(256) == 17)
            {
                c.EmitRaw("//E774: RANDOM COMPILER BUG\r\n");
                c.Emit("Lib.Fail(Messages.E774)");
            }

            //We only emit abstain guards for statements that are the target of 
            //an abstain, either by name or by gerund.
            if (s.AbstainSlot >= 0)
            {
                c.EmitRaw("if(abstainMap[" + s.AbstainSlot.ToString() + "])\n{\n");
            }

            if ((s.Percent > 0) && (s.Percent < 100))
            {
                c.EmitRaw("if(Lib.Rand(100)  < " + s.Percent.ToString() + ")\n{\n");
                c.EmitRaw(string.Format("    Trace.WriteLine(\"[{0:0000}] Rolled the dice and lost.\");\r\n", s.StatementNumber));
            }

            if (c.debugBuild)
            {
                c.EmitRaw(string.Format("Trace.WriteLine(\"[{0:0000}] {1}\");\n", s.StatementNumber, s.GetType().Name));
            }

            // Emit #line directive with a no-op for the debugger to stop on,
            // then hide the actual C# implementation. This ensures the debugger
            // stops once on each INTERCAL line even when the underlying C# calls
            // are marked [DebuggerNonUserCode].
            if (c.sourceFile != null && s.LineNumber >= 0)
            {
                c.EmitRaw("#line " + s.LineNumber + " \"" + c.sourceFile.Replace("\\", "\\\\") + "\"\r\n");
            }

        }

        public void EmitStatementEpilog(Statement s, CompilationContext c)
        {
            // Placeholder for debug local updates — replaced after all statements emitted
            c.EmitRaw("\r\n/*DEBUG_UPDATE_PLACEHOLDER*/\r\n");
            c.EmitRaw("#line hidden\r\n");

            //COME FROM statements don't include an abstain guard around
            //the COME FROM itself.  Any checks for abstaining or % prefixes
            //happen as part of processing the trap door below.
            if (s as Statement.ComeFromStatement == null)
            {
                if ((s.Percent < 100) && (s.Percent > 0))
                {
                    c.EmitRaw("}\n\n");
                    c.EmitRaw("else {");
                    c.EmitRaw(string.Format("    Trace.WriteLine(\"[{0:0000}] Rolled the dice and lost.\");", s.StatementNumber));
                    c.EmitRaw("}\r\n");
                }

                //Close off the abstain block
                if (s.AbstainSlot >= 0)
                {
                    c.EmitRaw("}\n\n");
                }
            }

            // Emit return label for NEXT statements BEFORE the COME FROM trapdoor
            // so that returning from a NEXT hits the trapdoor on the way through
            if (s is Statement.NextStatement ns2 && ns2.ReturnLabelId >= 0)
            {
                c.EmitRaw("_ret_" + ns2.ReturnLabelId + ": ;\r\n");
                c.EmitRaw("if (frame.ExecutionContext.Done) goto exit;\r\n");
            }

            //Now we handle COME FROM.  Note that even if the statement has
            //been ABSTAINED we still might fall through the trapdoor.  We have to
            //do this even for COME FROM statements in case someone is sick
            //enough to do this:
            //
            //(20) DO COME FROM (10)
            //(30) DO COME FROM (20)
            if (s.Trapdoor > 0)
            {
                Statement target = Statements[s.Trapdoor] as Statement;

                //We'll need to emit a label identifying the trapdoor, because if 
                //the line in question is a DO NEXT then when we return from the next
                //we have to evaluate the trapdoor before moving on to the next source line.
                //c.EmitRaw("trapdoor_" + s.StatementNumber + ":\n");

                //make sure the COME FROM in question has not been abstained!
                if (target.AbstainSlot >= 0)
                    c.EmitRaw("if(abstainMap[" + target.AbstainSlot + "])\n");

                //If the line is "DO %50 COME FROM" then we should jump 50 percent
                //of the time
                if ((target.Percent > 0) && (target.Percent < 100))
                {
                    c.EmitRaw("  if(lib.Rand(100) < " + target.Percent.ToString() + ")\n   ");
                }

                if (target.Label != null)
                    c.EmitRaw("    goto label_" + target.Label.Substring(1, target.Label.Length - 2) + ";\n");
                else
                    c.EmitRaw("    goto line_" + target.StatementNumber.ToString() + ";\n");
            }

            // When a statement is abstained, skip everything until the next
            // labeled statement to prevent fall-through in the goto model.
            // The else branch of the abstain guard jumps to the next label.
            // (The skip label is placed at the start of the next labeled statement's prolog)
            if (s.AbstainSlot >= 0 && s as Statement.ComeFromStatement == null)
            {
                c.EmitRaw("_abstain_skip_" + s.StatementNumber + ": ;\n");
            }

        }

        //This is the master routine for taking a program and emitting it as 
        //C#.
        public void EmitCSharp(CompilationContext c)
        {
            EmitProgramProlog(c);

            foreach (Statement s in Statements)
            {
                if(s is Statement.NextStatement)
                {
                    Stack<int> stack = new Stack<int>();

                    //Console.Write("Examing ({0}) for simple flow...", (s as Statement.NextStatement).Target);
                    //bool bSafe = IsSimpleFlow(s.StatementNumber,stack);
                    //Console.WriteLine(bSafe);

                    //If we determine that the flow is simple then we can
                    //optimize away the async call on the NEXTING stack and 
                    //replace it with an ordinary function call. Err...handling
                    //RESUME n with n>1 might turn out to be painful (but doable). 
                }

                EmitStatementProlog(s, c);
                if (s.Splatted)
                {
                    c.Warn("(" + s.LineNumber + ") * " + s.StatementText);
                }
                s.Emit(c);
                EmitStatementEpilog(s, c);
            }

            // Replace debug locals placeholder with actual declarations
            EmitDebugLocals(c);

            // Replace RESUME dispatch placeholder with goto switch
            EmitResumeDispatch(c);

            EmitProgramEpilog(c);
        }

        private static string DebugLocalName(string intercalName)
        {
            // .1 → dot_1, :1 → colon_1, ::1 → dcolon_1
            // ,1 → tail_1, ;1 → hybrid_1, ;;1 → dhybrid_1
            if (intercalName.StartsWith("::")) return "dcolon_" + intercalName.Substring(2);
            if (intercalName.StartsWith(";;")) return "dhybrid_" + intercalName.Substring(2);
            if (intercalName.StartsWith(":")) return "colon_" + intercalName.Substring(1);
            if (intercalName.StartsWith(";")) return "hybrid_" + intercalName.Substring(1);
            if (intercalName.StartsWith(".")) return "dot_" + intercalName.Substring(1);
            if (intercalName.StartsWith(",")) return "tail_" + intercalName.Substring(1);
            return "_" + intercalName;
        }

        private void EmitDebugLocals(CompilationContext c)
        {
            var sortedVars = c.DebugVariables.Where(v => v[0] == '.' || v[0] == ':').OrderBy(v => v).ToList();

            // Emit declarations
            var decls = new StringBuilder();
            foreach (var v in sortedVars)
            {
                decls.AppendLine("   ulong " + DebugLocalName(v) + " = 0;");
            }
            c.ReplaceMarker("/*DEBUG_LOCALS_PLACEHOLDER*/", decls.ToString());

            // Emit updates (same for every statement, hidden from debugger)
            var updates = new StringBuilder();
            updates.AppendLine("#line hidden");
            foreach (var v in sortedVars)
            {
                updates.AppendLine(DebugLocalName(v) + " = frame.ExecutionContext.GetVarValue(\"" + v + "\") ?? 0;");
            }
            c.ReplaceMarker("/*DEBUG_UPDATE_PLACEHOLDER*/", updates.ToString());
        }

        private void EmitResumeDispatch(CompilationContext c)
        {
            var sb = new StringBuilder();
            // Sentinel 0 = return from external entry (goto exit)
            sb.Append("case 0: goto exit; ");
            foreach (int id in c.NextReturnLabels)
            {
                sb.Append("case " + id + ": goto _ret_" + id + "; ");
            }
            c.ReplaceMarker("/*RESUME_DISPATCH_PLACEHOLDER*/", sb.ToString());
        }

        #endregion

        //Factory method for creating programs...
        public static Program CreateFromString(string src)
        {
            return new Program(src);
        }

        public static Program CreateFromFile(string file)
        {
            //First we parse statements into the Statements collections
            StreamReader srcFile = new StreamReader(file);
            string src = srcFile.ReadToEnd();
            srcFile.Close();

            return new Program(src);
        }
    }
}
