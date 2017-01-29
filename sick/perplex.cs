using System;
using System.IO;
using System.Text;
using INTERCAL.Runtime;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace INTERCAL
{
	//INTERCAL Expressions always evaluate to either an int16 or an int32.  For simplicity this
	//implementations treats everything as a 32-bit int except for some types of expressions
	//like mingle and select.
	
	abstract class Expression
	{
		//What does this expression return?  In classic intercal there are
		//only two possible types - Int16 and Int32.  For expressions this
		//will contain either typeof(Int16), typeof(Int32), or null (if the
		//type of the expression varies at runtime).
		protected Type returnType;

		public Type ReturnType { get { return returnType; }}
		
		public static Expression CreateExpression(Scanner s)
		{
			//the 'delimeter' argument is only used in some special
			//cases for arrays
			return CreateExpression(s, null).Optimize();
		}
		
		//This method might throw a ParseException for malformed
		//expressions.  This will be caught by CreateStatement which
		//will result in splatting the current statement.
		static Expression CreateExpression(Scanner s, string delimeter)
		{
			//<expression> ->
				//#digits    #12345
				//#<unary_operator><digits> #v12345 etc.
				//.<unary_operator>?digits	
				//,<unary_operator>?<digits>
				//:<unary_operator>?digits <subscript>
				//'<expression>'
				//"<expression>"
				//<expression><binary_op><expression>

			//<subscript> ->
			   //(SUB<expression>)*
			Expression retval = null;
			
			switch(s.Current.Value)
			{
				case "\"": 
				case "'":
					retval = new QuotedExpression(s);
					break;
				case "#":
					retval = new ConstantExpression(s);
					break;
				case ".":
				case ":":
					retval = new NumericExpression(s);
					break;
				case ",":
				case ";":
					retval = new ArrayExpression(s, delimeter);
					break;

				default:
					throw new ParseException(String.Format("line {0}: Invalid expression {1}", s.LineNumber, s.Current.Value));
			}

			//After we've read a valid expression if the next char is a
			//binary operator then we read the other expression and cojoin it
			//with this one.   
			if(s.PeekNext.Value == "$" || s.PeekNext.Value == "~")
			{
				s.MoveNext();
				string op = s.Current.Value;
				s.MoveNext();

				retval = new BinaryExpression(s,op,retval, Expression.CreateExpression(s));
			}


			return retval;
	}

		//After calling Evaluate callers can look at the return type to
		//decide if they need to trim down to 16 bits.
		public abstract uint Evaluate(ExecutionContext ctx);

		public abstract void Emit(CompilationContext ctx);

		//Optimize returns an optimized version of the expression.  For
		//example, #65535$65535 results in a binary expression - calling
		//optimize on it folds the constants together into a single constant
		//expression.
		public virtual Expression Optimize() 
		{
			//by default we do nothing.
			return this; 
		}

		class QuotedExpression : Expression
		{
			string delimeter; //either ' or "
			string unary_op = null;
			
			Expression child;

			public QuotedExpression(Scanner s)
			{
				delimeter = s.Current.Value;

				s.MoveNext();
				if(s.Current.Groups["unary_op"].Success)
				{
					//3.4.3 A unary operator is applied to a sparked or rabbit-eared 
					//expression by inserting the operator immediately following the opening spark or ears
					unary_op = s.Current.Value;
					s.MoveNext();
				}

				//We're alerting children that when they see a delimeter to return it to
				//us, not try to consume it for a new quoted expression.  This only matters
				//for arrays, where statements like:

				//	DO .1 <- ',4SUB#1'$',4SUB#2'

				child = Expression.CreateExpression(s, delimeter);

				s.MoveNext();

				Statement.VerifyToken(s, delimeter);

				this.returnType = child.ReturnType;
			}

			public override Expression Optimize() 
			{
				//first we optimize the child.
				child = child.Optimize();
				ConstantExpression c = child as ConstantExpression;
				
				//if the child expression is constant then we can
				//just compile-time evaluate our operator and return
				//a constant expression.
				if(c!= null)
				{
					if(unary_op == null)
						return child;
					else
					{
						if(c.Value < UInt16.MaxValue)
						{
							ushort tmp = (ushort)c.Value;
							switch(unary_op)
							{
								case "v": case "V":
									return new ConstantExpression(Lib.UnaryOr16(tmp));
								case "&": 
									return new ConstantExpression(Lib.UnaryAnd16(tmp));
								case "?": 
									return new ConstantExpression(Lib.UnaryXor16(tmp));
							}
						}
						else
						{
							switch(unary_op)
							{
								case "v": case "V":
									return new ConstantExpression(Lib.UnaryOr32(c.Value));
								case "&": 
									return new ConstantExpression(Lib.UnaryAnd32(c.Value));
								case "?": 
									return new ConstantExpression(Lib.UnaryXor32(c.Value));
							}
						}

					}
				}
				
				//if the child expression was not constant then we can't optimize.
				return this; 
			}

			public override void Emit(CompilationContext ctx)
			{
				ctx.EmitRaw("(");
				if(this.unary_op == null)
				{
					child.Emit(ctx);
				}
				else
				{
					switch(unary_op)
					{
						case "v": case "V":
							ctx.EmitRaw("Lib.Or(");
							child.Emit(ctx);
							ctx.EmitRaw(")");
							break;
						case "&":
							ctx.EmitRaw("Lib.And(");
							child.Emit(ctx);
							ctx.EmitRaw(")");
							break;
						case "?":
							ctx.EmitRaw("Lib.Xor(");
							child.Emit(ctx);
							ctx.EmitRaw(")");
							break;
					}
				}

				ctx.EmitRaw(")");
			}

			public override uint Evaluate(ExecutionContext ctx)
			{ 
				uint result = child.Evaluate(ctx);
				if(unary_op != null)
				{
					if(result < UInt16.MaxValue)
					{
						ushort tmp = (ushort)result;
						switch(unary_op)
						{
							case "v": case "V":
								result = (uint)Lib.UnaryOr16(tmp);
								break;
							case "&": 
								result = (uint)Lib.UnaryAnd16(tmp);
								break;
							case "?": 
								result = (uint)Lib.UnaryXor16(tmp);
								break;
						}
					}
					else
					{
						short tmp = (short)result;
						switch(unary_op)
						{
							case "v": case "V":
								result = (uint)Lib.UnaryOr32((uint)result);
								break;
							case "&": 
								result = (uint)Lib.UnaryAnd32((uint)result);
								break;
							case "?": 
								result = (uint)Lib.UnaryXor32((uint)result);
								break;
						}
					}
				}
				return result;
			}
		}
		
		class ConstantExpression : Expression
		{
			public readonly uint Value;
			protected static Dictionary<string, eval_delegate> eval_table = new Dictionary<string, eval_delegate>();
			
			protected delegate ushort eval_delegate(ushort val);

			static ConstantExpression()
			{
				eval_table["&"] = new eval_delegate(Lib.UnaryAnd16);
				eval_table["V"] = new eval_delegate(Lib.UnaryOr16);
				eval_table["v"] = new eval_delegate(Lib.UnaryOr16);
				eval_table["?"] = new eval_delegate(Lib.UnaryXor16);
			}

			//This constructor is used during optimization
			public ConstantExpression(uint val)
			{
				this.Value = val;
			}

			public ConstantExpression(Scanner s)
			{
				s.MoveNext();
				if(s.Current.Groups["unary_op"].Success)
				{
					//For some reason there's an unary operator in front of 
					//the digits.  We just do the conversion here...
					string op = s.Current.Value;
					s.MoveNext();
					Value = ((eval_delegate)eval_table[op])((ushort)UInt32.Parse(Statement.ReadGroupValue(s, "digits")));
				}
				else
				{
					Value = UInt32.Parse(Statement.ReadGroupValue(s, "digits"));
				}

				//Constant expressions are only 16 bits
				if(Value < UInt16.MinValue || Value > UInt16.MaxValue)
					//throw new ParseException(String.Format("line {0}: Constant too big (#{0})", s.LineNumber, Value));
					throw new CompilationException(Messages.E275);
				
				this.returnType = typeof(UInt16);
			}

			public override uint Evaluate(ExecutionContext ctx) { return Value; }
			
			public override void Emit(CompilationContext ctx)
			{
				ctx.EmitRaw(Value.ToString());
			}

		}
		
		//A numeric expression is an lvalue with an optional
		//unary operation on the front.
		class NumericExpression : Expression
		{
			delegate uint long_op(uint arg);
			delegate ushort short_op(ushort arg);

			string unary_op = null;
			string lval;
			
			long_op longform;
			short_op shortform;

			public NumericExpression(Scanner s)
			{
				string typeid = s.Current.Value;
				if(typeid == ":")
					this.returnType = typeof(UInt32);
				else if(typeid == ".")
					this.returnType = typeof(UInt16);
				else
					Debug.Assert(false);
				
				if(s.PeekNext.Groups["unary_op"].Success)
				{
					//Theres an unary operator
					s.MoveNext();
					unary_op = s.Current.Value;
					switch(unary_op)
					{
						case "v":
						case "V":
							this.shortform = new short_op(Lib.UnaryOr16);
							this.longform =  new long_op(Lib.UnaryOr32);
							break;
						case "?":
							this.shortform = new short_op(Lib.UnaryXor16);
							this.longform =  new long_op(Lib.UnaryXor32);
							break;
						case "&":
							this.shortform = new short_op(Lib.UnaryAnd16);
							this.longform =  new long_op(Lib.UnaryAnd32);
							break;
					}
				}
			
				s.MoveNext();
				this.lval = typeid + Statement.ReadGroupValue(s, "digits");
			}

			public override uint Evaluate(ExecutionContext ctx)
			{
				if(this.longform == null)
					return ctx[lval];
				/*
				switch(unary_op)
				{
						//This code commented out Jan 29, 2003 as being
						//redundant (I believe)
						
						case "v":
						case "V":
							this.shortform = new short_op(Lib.UnaryOr16);
							this.longform =  new long_op(Lib.UnaryOr32);
							break;
						case "?":
							this.shortform = new short_op(Lib.UnaryXor16);
							this.longform =  new long_op(Lib.UnaryXor32);
							break;
						case "&":
							this.shortform = new short_op(Lib.UnaryAnd16);
							this.longform =  new long_op(Lib.UnaryAnd32);
							break;
						

				}
				*/
				if(this.returnType == typeof(UInt16))
					return shortform((ushort)ctx[lval]);
				else
					return longform((uint)ctx[lval]);
			}
			
			public override void Emit(CompilationContext ctx)
			{
				if(this.longform == null)
					ctx.EmitRaw("frame.ExecutionContext[\"" + lval + "\"]");
				else
				{
					string sf = null;
					string lf = null;
					switch(unary_op)
					{
						case "v":
						case "V":
							sf = "Lib.UnaryOr16";
							lf =  "Lib.UnaryOr32";
							break;
						case "?":
							sf = "Lib.UnaryXor16";
							lf =  "Lib.UnaryXor32";
							break;
						case "&":
							sf = "Lib.UnaryAnd16";
							lf =  "Lib.UnaryAnd32";
							break;
						default:
							throw new CompilationException("Bad unary operator");

					}

					if(this.returnType == typeof(UInt16))
						ctx.EmitRaw(sf + "(((ushort)frame.ExecutionContext[\"" + lval + "\"]))");
						//return shortform((ushort)ctx[lval]);
					else
						ctx.EmitRaw(lf + "(((uint)frame.ExecutionContext[\"" + lval + "\"]))");
					//return longform((uint)ctx[lval]);
				}
			}
		}
			

		class ArrayExpression : Expression
		{
			string var;
			List<Expression> subscripts = new List<Expression>();
			int[] indices = null;

			public ArrayExpression(Scanner s) : this(s, null)
			{
			}
			
			//Consider an expression like ;1 SUB '#4$#2'~'#1$#1' #5.  When parsing the
			//expression '#4#2' we need to keep track of the fact that the second 
			//' is closing a previous one, not opening a new subscript as in ;3 SUB #4 '#3$#1'
			//DO .1 <- ',4SUB#1'$',4SUB#2'

			public ArrayExpression(Scanner s, string delimeter) 
			{
				//Array expressions are VAR SUB <subscript>*
				var = s.Current.Value + s.PeekNext.Value;
				s.MoveNext();
				
				if(s.PeekNext.Value == "SUB")
				{
					s.MoveNext(); //skip SUB
					
					//We can recognize a subscript coming by the presence of [.,;:#]					
					//If we see a ' or a " that matches delimeter then we know
					//that we are part of a larger expression and return
					char[] expression_starter = {'.', ',', ':', ';', '#', '\'', '"'};

					while((s.PeekNext.Value.IndexOfAny(expression_starter) != -1)  &&
						  (s.PeekNext.Value != delimeter))
					{
						s.MoveNext();
						subscripts.Add(Expression.CreateExpression(s, delimeter));
					}
				}
			}

			public override uint Evaluate(ExecutionContext ctx) 
			{
				string lval = var;

				//minor optimization - we reuse the same array object
				//every time instead of re-allocating it
				if(indices == null)
					indices = new int[subscripts.Count];

				for(int i=0; i< subscripts.Count; i++)
				{
					indices[i] = (int)(subscripts[i] as Expression).Evaluate(ctx);
				}
				
				return ctx[lval, indices];
			}

			public override void Emit(CompilationContext ctx)
			{
				ctx.EmitRaw("frame.ExecutionContext[\"" + this.var + "\", new int[]{");

				for(int i=0; i< subscripts.Count; i++)
				{
					ctx.EmitRaw("(int)");
					(subscripts[i] as Expression).Emit(ctx);

					if(i < (subscripts.Count-1))
					{
						ctx.EmitRaw(",");
					}
				}

				ctx.EmitRaw("}]");
			}
		}

		
		//This expression might return different types at different times...
		class BinaryExpression : Expression
		{
			string op;
			Expression Left; Expression Right;

			public BinaryExpression(Scanner s, string op, Expression left, Expression right)
			{
				this.op = op;
				Left = left;
				Right = right;

				if(op == "$")
				{
					returnType = typeof(UInt32);
				}
				else if(op == "~")
				{
						returnType = Right.ReturnType;
				}
				else if(op == "BY"){returnType = typeof(UInt32);}
				else
					throw new ParseException(String.Format("line {0}:Illegal operator {1}", s.LineNumber, s.Current.Value));
			}

			public override Expression Optimize()
			{
				Left = Left.Optimize();
				Right = Right.Optimize();

				ConstantExpression cleft = Left as ConstantExpression;
				ConstantExpression cright = Right as ConstantExpression;

				if((cleft != null) && (cright != null))
				{
					switch(op)
					{
						case "$":
							return new ConstantExpression(Lib.Mingle(cleft.Value, cright.Value));
						case "~":
							return new ConstantExpression((uint)Lib.Select(cleft.Value, cright.Value));
					}
				}

				//if both child expression are not constant then we can't fold them
				return this;
			}

			public override uint Evaluate(ExecutionContext ctx) 
			{
				uint a = Left.Evaluate(ctx);
				uint b = Right.Evaluate(ctx);

				switch(op)
				{
					case "$":
					{
						return Lib.Mingle((ushort)a, (ushort)b);
					}

					case "~":
					{
						//A select might use a 16-bit selector to select from a 32-bit
						//value.  No harm here if we pad the 16-bit value out to 32-bits,
						//select against the 32-bit value, then take the bottom 16 bits.
						return (uint)Lib.Select(a, b);
					}

					default:
						Lib.Fail(null);
						break;
				}

				return 0;
			}

			public override void Emit(CompilationContext ctx)
			{

				switch(op)
				{
					case "$":
					{
						ctx.EmitRaw("Lib.Mingle(");
						Left.Emit(ctx);
						ctx.EmitRaw(", ");
						Right.Emit(ctx);
						ctx.EmitRaw(")");
					}break;

					case "~":
					{
						//A select might use a 16-bit selector to select from a 32-bit
						//value.  No harm here if we pad the 16-bit value out to 32-bits,
						//select against the 32-bit value, then take the bottom 16 bits.
						ctx.EmitRaw("(uint)Lib.Select(");
						Left.Emit(ctx);
						ctx.EmitRaw(",");
						Right.Emit(ctx);
						ctx.EmitRaw(")");
					}break;

					default:
						Lib.Fail(null);
						break;
				}
			}

		}


		//This is a "special" expression that holds the result of
		//an array redimension. It doesn't return a uint when you
		//evaluate it; instead it returns a string of dimensions.
		//It's similar to an ArrayExpression, except that an
		//ArrayExpression boils down to a value.

		public class ReDimExpression : Expression
		{
			List<Expression> dimensions = new List<Expression>();

			public ReDimExpression(Scanner s, Expression first)
			{
				dimensions.Add(first);

				while(s.PeekNext.Value == "BY")
				{
					s.MoveNext();
					s.MoveNext();
					dimensions.Add(Expression.CreateExpression(s));
				}

			}
			
			public override uint Evaluate(ExecutionContext ctx) 
			{
				throw new Exception("Don't call this!");
			}

			public int[] GetDimensions(ExecutionContext ctx)
			{
				int[] retval = new int[dimensions.Count];
				
				for(int i=0; i<dimensions.Count; i++)
				{
					retval[i] = (int)(dimensions[i] as Expression).Evaluate(ctx);
				}

				return retval;
			}

			public override void Emit(CompilationContext ctx)
			{
				ctx.EmitRaw("new int[] {");

				for(int i=0; i<dimensions.Count; i++)
				{
					//retval[i] = (int)(dimensions[i] as Expression).Evaluate(ctx);
					ctx.EmitRaw("(int)");
					(this.dimensions[i] as Expression).Emit(ctx);
					if(i < dimensions.Count-1)
						ctx.EmitRaw(",");
				}

				ctx.EmitRaw("}");
			}

		}

	}

	public class LValue 
	{
		//An lval is a lot like an expression, except resolving it results
		//in a string naming a location.  So for example :1 is an lval, but
		//so is ;1SUB:1.  The latter expression might resolve to a different
		//location every time.  
		string VarName;

		//Most lvals will not have subscripts, but 
		//arrays obviously do, so we track them in an list of expressions.
		List<Expression> subscripts = null;

		public LValue(Scanner s)
		{
			//first read the basic lval...
			VarName = Statement.ReadGroupValue(s, "var");
			if(s.Current.Value == "#")
				throw new ParseException(String.Format("line {0}: Constants cannot be used as lvalues ", s.LineNumber));
			
			s.MoveNext();
			VarName += Statement.ReadGroupValue(s, "digits");
			
			//Now look to see if we have any subscripts.
			if(s.PeekNext.Value == "SUB")
			{
				subscripts = new List<Expression>();
				s.MoveNext(); //skip SUB
					
				//We can recognize a subscript coming by the presence of [.,;:#]					
				char[] expression_starter = {'.', ',', ':', ';', '#', '\'', '"'};
				while(s.PeekNext.Value.IndexOfAny(expression_starter) != -1)
				{
					s.MoveNext();
					subscripts.Add(Expression.CreateExpression(s));
				}
			}
		}

		public bool IsArray
		{
			get { if(Name[0] == ',' || Name[0] == ';')
					  return true;
				  else 
					  return false;
			    }
		}
		public bool Subscripted{ get { if(subscripts == null) return false; else return true; }}
		public string Name { get { return VarName; }}		
		public int[] Subscripts(ExecutionContext ctx)
		{
			if(subscripts == null)
				return null;
			
			int[] indices = new int[subscripts.Count];

			for(int i=0; i<subscripts.Count; i++)
			{
				indices[i] = (int)(subscripts[i] as Expression).Evaluate(ctx);
			}

			return indices;
		}

		//This emits an expression that will hold subscripts, e.g. "new int[] {2,2}"
		public void EmitSubscripts(CompilationContext ctx)
		{
			ctx.EmitRaw("new int[] {");
			for(int i=0; i<subscripts.Count; i++)
			{
				ctx.EmitRaw("(int)");
				(subscripts[i] as Expression).Emit(ctx);
				if(i < subscripts.Count -1)
					ctx.EmitRaw(",");
			}
			ctx.EmitRaw("}");
		}
	}
}