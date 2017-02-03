using System;
using INTERCAL.Runtime;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace INTERCAL
{
	//When a parsing error is detected a ParseException is thrown.
	class ParseException : System.Exception
	{
		public ParseException() : base(null){}
		public ParseException(string msg) : base(msg){}
	}

	//This is used to hold a list of entry points exported from 
	//an assembly.
	public class ExportList
	{
		public string assemblyFile;
		public Assembly assembly;
		public EntryPointAttribute[] entryPoints;

		public ExportList(string assemblyFile)
		{
			this.assemblyFile = assemblyFile;
			try
			{
				assembly = Assembly.LoadFrom(assemblyFile);

				entryPoints = (EntryPointAttribute[])assembly.GetCustomAttributes(typeof(EntryPointAttribute), true);
			
			}
			catch(Exception e)
			{
				throw new CompilationException(Messages.E2002, e);
			}
		}
	}


	public abstract class Statement 
	{
	#region fields
		//Every statement remembers its line number in the source file.
		public int LineNumber = 0;

		//Which statement is this?
		public int StatementNumber = 0;

		//This is used for COME FROM.  if this contains something other than -1
		//then it contains the index of the destination COME FROM.
		public int Trapdoor = -1;

		//A statement may begin with a logical line label enclosed in wax-wane pairs (()). 
		//A statement may not have more than one label, although it is possible to omit the label 
		//entirely. A line label is any integer from 1 to 65535, which must be unique within each
		//program. The user is cautioned, however, that many line labels between 1000 and 
		//1999 are used in the INTERCAL System Library functions. 
		public string  Label = null;
		
		//Unrecognizable statements, as noted in section 9, are flagged with a splat (*)
		//during compilation, and are not considered fatal errors unless they are encountered 
		//during execution, at which time the statement (as input at compilation time) is 
		//printed and execution is terminated.
		public bool Splatted = false;

		//After the line label (if any), must follow one of the following statement identifiers: DO, PLEASE, or 
		///PLEASE DO. These may be used interchangeably to improve the aesthetics of the program. 
		//The identifier is then followed by either, neither, or both of the following optional parameters (qualifiers): 
		//(1) either of the character strings NOT or N'T, which causes the statement to be automatically abstained from (see section 4.4.9) 
		public bool bPlease = false;
		public bool bEnabled =true;
		
		//when execution begins, and (2) a number between 0 and 100, preceded by a double-oh-seven (%), which causes the statement to 
		//have only the specified percent chance of being executed each time it is encountered in the course of execution. 
		public int Percent;

		//The text of this statement...
		public string StatementText;

		//This variable is both assigned to and read from the back end of the compiler.
		//When EmitProgramProlog generates the abstain map it will check each statement
		//to see if it is a target of an abstain or a reinstate.  If it is then it will
		//emit an entry for it in the abstain map and record the entry in AbstainSlot, so
		//that later when it emits the abstain guard it can reference the right slot.
		public int AbstainSlot = -1;

	#endregion

		public abstract void Emit(CompilationContext ctx);

		public static string ReadGroupValue(Scanner s, string group)
		{
			if(s.Current.Groups[group].Success)
				return s.Current.Groups[group].Value;
			else
				throw new ParseException(String.Format(Messages.E017,s.LineNumber+1));
		}

		public static void VerifyToken(Scanner s, string val)
		{
			if(s.Current.Value != val)
				throw new ParseException(String.Format(Messages.E017,s.LineNumber+1));

		}
		//factory method that takes a line of input and creates a statement
		//object
		public static Statement CreateStatement(Scanner s)
		{
			//Remember what line we started on in case the
			//statement spans lines.
			
			int line = s.LineNumber;
			bool please = false;
			bool enabled = true;
			int  percent = 100;

			Statement retval = null;
			string Label     = null;
			

			try
			{
				//First we look to see if there is a label...

				if(s.Current.Groups["label"].Success)
				{
					Label = Statement.ReadGroupValue(s, "label");
					s.MoveNext();
				}

				bool validPrefix = false;



				//Next we expect either DO, PLEASE, or PLEASE DO
				if(s.Current.Value == "PLEASE")
				{
					validPrefix = true;
					please = true;
					s.MoveNext();
				}
				
				//If they've said PLEASE then they don't *have* to
				//use DO.  Unless they plan on doing a DON'T or DO NOT
				if(s.Current.Value == "DO")
				{
					validPrefix = true;
					s.MoveNext();
					
					if(s.Current.Value == "NOT" ||
						s.Current.Value == "N'T")
					{
						enabled = false;
						s.MoveNext();
					}
				}
				

				//Finally the user might put a %50 here.  Note that
				//even if the statement is disabled we need to remember
				//the % on the off chance that the statement gets enabled
				//later.
				if(s.Current.Value == "%")
				{
					s.MoveNext();
					string p = Statement.ReadGroupValue(s, "digits");
					percent = Int32.Parse(p);
					s.MoveNext();
				}


				//Here we parse out the statement prefix.  Easier to
				//do it here than break out a separate function.
				
				while(s.Current.Groups["prefix"].Success)
				{
					switch(s.Current.Value)
					{
						case "DO" : 
							validPrefix = true; 
							break;

						case "PLEASE":
							validPrefix = true;
							please = true;
							break;

						case "NOT":
						case "N'T":
							enabled = false;
							break;
						
						case "%":
							s.MoveNext();
							string p = Statement.ReadGroupValue(s, "digits");
							percent = Int32.Parse(p);
							break;


					}
					s.MoveNext();
				}
				

				if(!validPrefix) 
					throw new ParseException(String.Format(Messages.E017,s.LineNumber+1));

				if(s.Current.Groups["statement"].Success)
				{
					//We are looking at the beginning of a statement
					switch(s.Current.Value)
					{
						case "ABSTAIN FROM":retval = new AbstainStatement(s);	break;
						case "READ OUT":	retval = new ReadOutStatement(s);	break;
						case "WRITE IN":	retval = new WriteInStatement(s);	break;
						case "COME FROM":	retval = new ComeFromStatement(s);	break;	
						case "REINSTATE":	retval = new ReinstateStatement(s);	break;
						case "STASH":		retval = new StashStatement(s);		break;
						case "RESUME":		retval = new ResumeStatement(s);	break;
						case "FORGET":		retval = new ForgetStatement(s);	break;
						case "IGNORE":		retval = new IgnoreStatement(s);	break;
						case "REMEMBER":	retval = new RememberStatement(s);	break;
						case "RETRIEVE":	retval = new RetrieveStatement(s);	break;
						case "GIVE UP":		retval = new GiveUpStatement(s);	break;
					}
				}
				else if(s.Current.Groups["label"].Success)
				{
					retval = new NextStatement(s);
				}
				else if(s.Current.Groups["var"].Success)
				{
					retval = new CalculateStatement(s);
				}
				else
				{
					throw new ParseException(String.Format(Messages.E017,s.LineNumber+1));
				}


				//Move on to what should be the beginning of the next statement
				s.MoveNext();
			}
			catch(ParseException)
			{
				//Console.WriteLine(p.Message);

				if(retval != null) 
					retval.Splatted = true;
				
				else retval = new NonsenseStatement(s);

				s.Panic();
			}

			//Note that even badly formed statements get their labels set. This way
			//you can still jump to them (though that will cause an exception).
			if(Label != null)
				retval.Label = Label;
			
			retval.LineNumber = line;
			retval.bEnabled = enabled;
			retval.bPlease = please;
			retval.Percent = percent;

			return retval;

		}

		#region Statement definitions
		//Every type of statement in Intercal has an associated class
		//that extends base Statement. Execution consists of calling
		//Evaluate on each statement in succession.  

		public class CalculateStatement : Statement
		{
			LValue destination;
			Expression expression;
			bool IsArrayRedimension = false;

			public CalculateStatement(Scanner s) 
			{
				destination = new LValue(s);

				s.MoveNext();
				VerifyToken(s, "<-");
				s.MoveNext();
				
				expression = Expression.CreateExpression(s);

				//Is this an array redimension expression?
				if((destination.IsArray) && (!destination.Subscripted))
				{
					this.IsArrayRedimension = true;
					expression = new Expression.ReDimExpression(s,expression);
				}

			}
			
			public override void Emit(CompilationContext ctx)
			{
				//Basically we expect to see <lvalue> <- <Expression>
				if(!IsArrayRedimension)
				{
					if(ctx.debugBuild)
					{
             
						ctx.EmitRaw(string.Format("Trace.WriteLine(string.Format(\"       {0} <- {{0}}\",", destination.Name));
						expression.Emit(ctx); 
						ctx.EmitRaw("));\r\n");
					}

					string lval = destination.Name;
					if(!destination.Subscripted)
					{
						ctx.EmitRaw("frame.ExecutionContext[\"" + lval + "\"] = ");
						expression.Emit(ctx);
						ctx.EmitRaw(";\n");
					}
					else
					{
						//ctx[lval, destination.Subscripts(ctx)] = expression.Evaluate(ctx);
						ctx.EmitRaw("frame.ExecutionContext[\"" + destination.Name + "\", ");
						destination.EmitSubscripts(ctx);
						ctx.EmitRaw("] = ");
						expression.Emit(ctx);
						ctx.EmitRaw(";\n");

						//Debug.Assert(false);
					}
				}
				else
				{
					ctx.EmitRaw("frame.ExecutionContext.ReDim(\"" + this.destination.Name + "\",");
					expression.Emit(ctx);
					ctx.EmitRaw(");\n");
				}

			}

		}

		public class NextStatement : Statement
		{
			public string Target;

			public NextStatement(Scanner s) 
			{
				this.Target = ReadGroupValue(s, "label");
				s.MoveNext();
				VerifyToken(s, "NEXT");
			}
			
			void EmitExternalCall(CompilationContext ctx)
			{
				try
				{
					//If we are referencing a label in another assembly then we 
					//need to figure out what assembly that label lives in.  We'll do
					//this with a dumb ol' search.
				

					foreach(ExportList e in ctx.references)
					{
						foreach(EntryPointAttribute a in e.entryPoints)
						{
							if(a.Label == this.Target)
							{
								Type t = e.assembly.GetType(a.ClassName);
								MethodInfo m = t.GetMethod(a.MethodName, new Type[] { typeof(ExecutionContext) } );
								
								if(m != null)
								{
                                    if (m.IsStatic)
                                    {
                                        //ctx.EmitRaw(a.ClassName + "." + a.MethodName + "(frame.ExecutionContext);");

                                        ctx.EmitRaw("{\r\n");
                                        ctx.EmitRaw(string.Format("   bool shouldTerminate = {0}.{1}(frame.ExecutionContext);\r\n", a.ClassName, a.MethodName));
                                        ctx.EmitRaw("   if (shouldTerminate)\r\n");
                                        ctx.EmitRaw("   {\r\n");
                                        ctx.EmitRaw("       goto exit;\r\n");
                                        ctx.EmitRaw("   }\r\n");

                                        if (ctx.debugBuild) {
                                            ctx.EmitRaw("   else\r\n");
                                            ctx.EmitRaw("   {\r\n");
                                            ctx.EmitRaw(string.Format("      Trace.WriteLine(\"Resuming execution at {0}\");", StatementNumber));
                                            ctx.EmitRaw("   }\r\n");
                                        }

                                        ctx.EmitRaw("}\r\n");

                                    }
                                    else
                                    {
                                        if (!ctx.ExternalReferences.Contains(a.ClassName))
                                            ctx.ExternalReferences.Add(a.ClassName);

                                        ctx.EmitRaw("{\r\n");
                                        ctx.EmitRaw("   bool shouldTerminate = " + ctx.GeneratePropertyName(a.ClassName) + "." + a.MethodName + "(frame.ExecutionContext);\r\n");
                                        ctx.EmitRaw("   if (shouldTerminate)\r\n");
                                        ctx.EmitRaw("   {\r\n");
                                        ctx.EmitRaw("       goto exit;\r\n");
                                        ctx.EmitRaw("   }\r\n");

                                        if (ctx.debugBuild)
                                        {
                                            ctx.EmitRaw("   else\r\n");
                                            ctx.EmitRaw("   {\r\n");
                                            ctx.EmitRaw(string.Format("      Trace.WriteLine(\"Resuming execution at {0}\");", StatementNumber));
                                            ctx.EmitRaw("   }\r\n");
                                        }

                                        ctx.EmitRaw("}\r\n");

                                    }
                                }

								else
								{
									//look for the dynamic one.
									m = t.GetMethod(a.MethodName, new Type[] { typeof(ExecutionContext) });
									if(m != null)
									{
										if(m.IsStatic)
                                        {
                                            ctx.EmitRaw("{\r\n");
                                            ctx.EmitRaw(string.Format("   bool shouldTerminate = {0}.{1}(frame.ExecutionContext);\r\n", a.ClassName, a.MethodName));
                                            ctx.EmitRaw("   if (shouldTerminate)\r\n");
                                            ctx.EmitRaw("   {\r\n");
                                            ctx.EmitRaw("       goto exit;\r\n");
                                            ctx.EmitRaw("   }\r\n");

                                            if (ctx.debugBuild)
                                            {
                                                ctx.EmitRaw("   else\r\n");
                                                ctx.EmitRaw("   {\r\n");
                                                ctx.EmitRaw(string.Format("      Trace.WriteLine(\"Resuming execution at {0}\");", StatementNumber));
                                                ctx.EmitRaw("   }\r\n");
                                            }

                                            ctx.EmitRaw("}\r\n");
                                        }
										else
										{
											if(!ctx.ExternalReferences.Contains(a.ClassName))
												ctx.ExternalReferences.Add(a.ClassName);

                                            ctx.EmitRaw("{\r\n");
                                            ctx.EmitRaw("   bool shouldTerminate = " + ctx.GeneratePropertyName(a.ClassName) + "." + a.MethodName + "(frame.ExecutionContext);\r\n");
                                            ctx.EmitRaw("   if (shouldTerminate)\r\n");
                                            ctx.EmitRaw("   {\r\n");
                                            ctx.EmitRaw("       goto exit;\r\n");
                                            ctx.EmitRaw("   }\r\n");

                                            if (ctx.debugBuild)
                                            {
                                                ctx.EmitRaw("   else\r\n");
                                                ctx.EmitRaw("   {\r\n");
                                                ctx.EmitRaw(string.Format("      Trace.WriteLine(\"Resuming execution at {0}\");", StatementNumber));
                                                ctx.EmitRaw("   }\r\n");
                                            }

                                            ctx.EmitRaw("}\r\n");

                                        }
                                    }
									else
										throw new CompilationException(String.Format(Messages.E2004, a.ClassName, a.MethodName));

								}
								return;
							}
						}
					}
				}
				catch(CompilationException)
				{
    				throw;
				}

				catch(Exception)
				{
					ctx.Warn(Messages.E129 + this.Target);
					ctx.EmitRaw("Lib.Fail(Messages.E129 + \""+ this.Target +"\");\n");
				}

				
				ctx.Warn(Messages.E129 + this.Target);
				ctx.EmitRaw("Lib.Fail(Messages.E129+ \"" + this.Target + "\");\n");
			}

			public override void Emit(CompilationContext ctx)
			{
				if(ctx.program[this.Target].Count() == 0)
				{
                    //the passed label isn't a local label
                    ctx.Emit(string.Format("Trace.WriteLine(\"       Doing {0} Next\");", this.Target));
                    EmitExternalCall(ctx);
				}

				else
				{
					Statement target = ctx.program[this.Target].First() as Statement;

                    if (!string.IsNullOrEmpty(target.Label))
                    {
                        ctx.Emit(string.Format("Trace.WriteLine(\"       Doing {0} Next\");", target.Label));
                    }
                    else
                    {
                        ctx.Emit(string.Format("Trace.WriteLine(\"       Doing statement #{0} Next\");", target.StatementNumber));

                    }

                    //ctx.Emit("nest = this.Eval(ref ctx," +  target.StatementNumber + ")");

                    //Console.WriteLine("[{0}] (000) DO NEXT (100)", Thread.CurrentThread.ManagedThreadId);

                    ctx.EmitRaw("{\r\n");
                    ctx.EmitRaw("   bool shouldTerminate = frame.ExecutionContext.Evaluate(Eval," + target.Label.Substring(1, target.Label.Length -2) + ");\r\n");
                    ctx.EmitRaw("   if (shouldTerminate)\r\n");
                    ctx.EmitRaw("   {\r\n");
                    ctx.EmitRaw("       goto exit;\r\n");
                    ctx.EmitRaw("   }\r\n");

                    if (ctx.debugBuild)
                    {
                        ctx.EmitRaw("   else\r\n");
                        ctx.EmitRaw("   {\r\n");
                        ctx.EmitRaw(string.Format("      Trace.WriteLine(\"Resuming execution at {0}\");", StatementNumber));
                        ctx.EmitRaw("   }\r\n");
                    }

                    ctx.EmitRaw("}\r\n");

                }
            }
		}

		public class ForgetStatement : Statement
		{
			Expression exp;

			public ForgetStatement(Scanner s)
			{
				s.MoveNext();
				exp = Expression.CreateExpression(s);
			}

			public override void Emit(CompilationContext ctx)
			{
                //CRAP CRAP CRAP.  FORGET drops entries from
                //the stack.  If a program says DO FORGET #1 / DO RESUME #1
                //we should do the same thing as if they said "RESUME #2".
                //I think the easiest way to do this is to hold an "adjustment"
                //variable - every time you say "FORGET <expr> we add the 
                //result of <expr> to the adjuster.  Whenever a RESUME
                //is encountered then we take what we would have returned
                //out of RESUME and add the value of the adjuster. 


                //So initially all I did was to add 1 to the number of
                //forgets.  forget is a uint because all the assignments
                //added to it are also uints and this avoids a ton of 
                //casting.  Just adding one doesn't work because it's
                //legal to underflow the stack, so depth-forget might
                //evaluate to less than zero. Thus the extra if here
                //which handles NEXT stack underflow.
                if (ctx.debugBuild)
                {
                    ctx.EmitRaw("Trace.WriteLine(\"       Forgetting ");
                    this.exp.Emit(ctx);
                    ctx.EmitRaw("\");\r\n");
                }

                ctx.EmitRaw("frame.ExecutionContext.Forget(");
                this.exp.Emit(ctx);
                ctx.EmitRaw(");\r\n");
			}
		}
		
		public class ResumeStatement : Statement
		{
			Expression Depth;

			public ResumeStatement(Scanner s)
			{
				s.MoveNext(); 
				if(s.PeekNext.Groups["prefix"].Success || s.PeekNext.Groups["label"].Success)
					return;
				
				Depth = Expression.CreateExpression(s);
			}

			public override void Emit(CompilationContext ctx)
			{
                //RESUME 0 needs to be treated as a no-op.
                ctx.EmitRaw("   {\r\n");
                ctx.EmitRaw("      uint depth = ");
                Depth.Emit(ctx);
                ctx.EmitRaw(";\r\n");

                if (ctx.debugBuild)
                {
                    ctx.Emit("      Trace.WriteLine(string.Format(\"      Resuming {0}\", depth));");
                }


                ctx.EmitRaw("      if(depth > 0)\r\n");
                ctx.EmitRaw("      {\r\n");


                ctx.EmitRaw("         frame.ExecutionContext.Resume(depth);\r\n");
                ctx.EmitRaw("         goto exit;\r\n");
                ctx.EmitRaw("      }\r\n");
                ctx.EmitRaw("   }\r\n");
            }
        }

		public class StashStatement : Statement
		{
			protected internal List<LValue> lvals = new List<LValue>();

			public StashStatement(Scanner s)
			{
				s.MoveNext();

				LValue lval = new LValue(s);
				lvals.Add(lval);

				while(s.PeekNext.Value == "+")
				{
					s.MoveNext();
					s.MoveNext();
					lvals.Add(new LValue(s));
				}
			}

			public override void Emit(CompilationContext ctx)
			{
				foreach(LValue lval in lvals)
				{
                    ctx.Emit(string.Format("Trace.WriteLine(\"       Stashing {0}\");", lval.Name));
					ctx.Emit("frame.ExecutionContext.Stash(\"" + lval.Name+ "\")");
				}
			}
		}		

		public class RetrieveStatement : StashStatement
		{
			public RetrieveStatement(Scanner s) : base(s){}
			public override void Emit(CompilationContext ctx)
			{
				foreach(LValue lval in lvals)
				{
                    ctx.Emit(string.Format("Trace.WriteLine(\"       Retrieving {0}\");", lval.Name));
                    ctx.Emit("frame.ExecutionContext.Retrieve(\"" + lval.Name+ "\")");
				}
			}

		}

		
		public class IgnoreStatement : Statement
		{
			protected List<LValue> Targets = new List<LValue>();

			public IgnoreStatement(Scanner s)
			{
				for(;;)
				{
					s.MoveNext();
					LValue target = new LValue(s);

					Targets.Add(target);
					if (s.PeekNext.Value != "+")
						break;
				}
			}

			public override void Emit(CompilationContext ctx)
			{
				foreach(LValue lval in Targets)
				{
					ctx.Emit("frame.ExecutionContext.Ignore(\"" + lval.Name+ "\")");
				}
			}

		}

		public class RememberStatement : IgnoreStatement
		{
			public RememberStatement(Scanner s) : base (s)	{}

			public override void Emit(CompilationContext ctx)
			{
				foreach(LValue lval in Targets)
				{
					ctx.Emit("frame.ExecutionContext.Remember(\"" + lval.Name+ "\")");
				}
			}

		}

		public class AbstainStatement : Statement
		{
			//One or the other of these will be non-null
			public  List<string> Gerunds = null;
			public  string Target = null;

			public AbstainStatement(Scanner s)
			{
				s.MoveNext();
				if(s.Current.Groups["gerund"].Success)
				{
                    Gerunds = new List<string>() { s.Current.Value };
							
					while(s.PeekNext.Value == "+")
					{
						s.MoveNext();
						s.MoveNext();
						Gerunds.Add(ReadGroupValue(s,"gerund"));
					}
				}
				else if(s.Current.Groups["label"].Success)
				{
					Target = s.Current.Value;
				}
				
				else throw new ParseException(String.Format("line {0}: Invalid statement", s.LineNumber));

			}

			public override void Emit(CompilationContext ctx)
			{
				if(Target != null)
				{
                    Statement t = ctx.program[Target].FirstOrDefault();
                    if(t != null)
                    { 
						Debug.Assert(t.Label == Target);
						Debug.Assert(t.AbstainSlot >= 0);

						ctx.EmitRaw("abstainMap[" + t.AbstainSlot + "] = false;\n");
					}
					else
					{
						ctx.Emit("Lib.Fail(\"" + Messages.E139 + Target + "\")");
					}
				}
				else
				{
					foreach(string t in Gerunds)
					{
						foreach(Statement r in ctx.program.Statements)
						{
							if(r.GetType() == CompilationContext.AbstainMap[t])
							{
								ctx.EmitRaw("abstainMap[" + r.AbstainSlot + "] = false;\n");
							}
						}
					}
				}
			}
		}

		public class ReinstateStatement : AbstainStatement
		{
			public ReinstateStatement(Scanner s) : base(s)
			{
			}

			public override void Emit(CompilationContext ctx)
			{
				if(Target != null)
				{
					Statement t = ctx.program[Target].First();
                    Debug.Assert(t.Label == Target);
					Debug.Assert(t.AbstainSlot >= 0);
					ctx.EmitRaw("abstainMap[" + t.AbstainSlot + "] = true;\n");
				}
				else
				{
					foreach(string t in Gerunds)
					{
						foreach(Statement r in ctx.program.Statements)
						{
							if(r.GetType() == CompilationContext.AbstainMap[t])
							{
								ctx.EmitRaw("abstainMap[" + r.AbstainSlot + "] = true;\n");
							}
						}
					}
				}
			}
		}

		public class ReadOutStatement : Statement
		{
			List<Expression> lvals = new List<Expression>();

			public ReadOutStatement(Scanner s)
			{
				s.MoveNext();
				lvals.Add(Expression.CreateExpression(s));
				while(s.PeekNext.Value == "+")
				{
					s.MoveNext();
					s.MoveNext();
					lvals.Add(Expression.CreateExpression(s));
				}

			}

			public override void Emit(CompilationContext ctx)
			{
				foreach(Expression lval in lvals)
				{
                    var ae = lval as Expression.ArrayExpression;

                    bool shortCircuitArray = false;

                    if (ae != null)
                    {
                        if (ae.Indices == null || ae.Indices.Length == 0)
                            shortCircuitArray = true;
                    }

                    if(shortCircuitArray)
                    {
                        ctx.EmitRaw(
                            string.Format("frame.ExecutionContext.ReadOut(\"{0}\");", ae.Name));
                    }
                    else
                    {
                        ctx.EmitRaw("frame.ExecutionContext.ReadOut(");
                        lval.Emit(ctx);
                        ctx.EmitRaw(");");
                    }
				}
			}
		}
		public class WriteInStatement : Statement
		{
            protected List<LValue> lvals = new List<LValue>();

            public WriteInStatement(Scanner s)
            {
                s.MoveNext();
                lvals.Add(new LValue(s));
                while (s.PeekNext.Value == "+")
                {
                    s.MoveNext();
                    s.MoveNext();
                    lvals.Add(new LValue(s));
                }
            }

			public override void Emit(CompilationContext ctx)
			{
				foreach(LValue lval in lvals)
				{
					ctx.Emit("frame.ExecutionContext.WriteIn(\"" + lval.Name + "\")");
				}
			}
		}

		public class GiveUpStatement : Statement
		{
			public GiveUpStatement(Scanner s)	{ }

			public override void Emit(CompilationContext ctx)
			{
                //-1 means "unconditional return"
                ctx.Emit("           frame.ExecutionContext.GiveUp();\r\n");
			}
		}

		public class NonsenseStatement : Statement
		{
			public NonsenseStatement(Scanner s)
			{
				this.LineNumber = s.LineNumber;
				this.Splatted = true; 
			}

			public override void Emit(CompilationContext ctx)
			{
                //That showoffy jerk Donald Knuth just *had* to put a quote in a 
                //multiline comment so now I have to fix those up too.
                var fixedUp = StatementText.Replace("\"", "\\\"").Replace("\r\n", "\" + \r\n\"");
				ctx.EmitRaw("Lib.Fail(\""+ LineNumber.ToString() + " * " + fixedUp);
				ctx.EmitRaw("\");\n");
			}
		}

		public class ComeFromStatement : Statement
		{
			public string Target = null;

			public override void Emit(CompilationContext ctx)
			{
				//We don't have to emit any code - not even this NOP
				//because something will always a COME FROM.  Thus
				//all we wind up emitting is a label.  We don't 
				//actually emit the label here - it gets emitted in
				//Program.EmitStatementProlog so it can integrate
				//with the ABSTAIN / REINSTATE machinery.
				
			}

			public ComeFromStatement(Scanner s)
			{
				s.MoveNext();
				Target = ReadGroupValue(s, "label");
			}
		}
		

		public class SentinelStatement : Statement
		{
			public string Target = null;

			public override void Emit(CompilationContext ctx)
			{
				ctx.Emit("throw new IntercalException(Messages.E633)");
			}

		}
	#endregion
	}
}