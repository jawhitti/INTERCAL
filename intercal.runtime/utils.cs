using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Collections.Generic;
using System.Threading;

namespace INTERCAL
{
    namespace Runtime
    {
        public class Messages
        {
            /*
                 * Note: these error message texts, with one exception, are direct from 
                 * the Princeton compiler (INTERCAL-72) sources (transmitted by Don Woods).
                 * The one exception is E632, which in INTERCAL-72 had the error message
                 *	PROGRAM ATTEMPTED TO EXIT WITHOUT ERROR MESSAGE
                 * ESR's "THE NEXT STACK HAS RUPTURED!..." has been retained on the grounds
                 * that it is more obscure and much funnier.  For context, find a copy of
                 * Joe Haldeman's SF short story "A !Tangled Web", first published in 
                 * Analog magazine sometime in 1983 and later anthologized in the author's
                 * "Infinite Dreams" (Ace 1985).
                 */
            /* An undecodable statement has been encountered in the course of execution. */
            public const string E000 = "E000 %s";
            /* An expression contains a syntax error. */
            public const string E017 = "E017 DO YOU EXPECT ME TO FIGURE THIS OUT?\n   ON THE WAY TO {0}";
            /* DONE Improper use has been made of statement identifiers. */
            public const string E079 = "E079 PROGRAMMER IS INSUFFICIENTLY POLITE";
            /* DONE Improper use has been made of statement identifiers. */
            public const string E099 = "E099 PROGRAMMER IS OVERLY POLITE";
            /* DONE Program has attempted 80 levels of NEXTing */
            public const string E123 = "E123 PROGRAM HAS DISAPPEARED INTO THE BLACK LAGOON";
            /* DONE Program has attempted to transfer to a non-existent line label */
            public const string E129 = "E129 PROGRAM HAS GOTTEN LOST ON THE WAY TO ";
            /* DONE An ABSTAIN or REINSTATE statement references a non-existent line label */
            public const string E139 = "E139 I WASN'T PLANNING TO GO THERE ANYWAY";
            /* DONE A line label has been multiply defined. */
            public const string E182 = "E182 YOU MUST LIKE THIS LABEL A LOT!";
            /* DONE An invalid line label has been encountered. */
            public const string E197 = "E197 SO!  65535 LABELS AREN'T ENOUGH FOR YOU?";
            /* An expression involves an unidentified variable. */
            public const string E200 = "E200 NOTHING VENTURED, NOTHING GAINED";
            /* An attempt has been made to give an array a dimension of zero. */
            public const string E240 = "E240 ERROR HANDLER PRINTED SNIDE REMARK";
            /* DONE Invalid dimensioning information was supplied in defining or using an array. */
            public const string E241 = "E241 VARIABLES MAY NOT BE STORED IN WEST HYPERSPACE";
            /* DONE A 32-bit value has been assigned to a 16-bit variable. */
            public const string E275 = "E275 DON'T BYTE OFF MORE THAN YOU CAN CHEW";
            /* DONE A retrieval has been attempted for an unSTASHed value. */
            public const string E436 = "E436 THROW STICK BEFORE RETRIEVING!";
            /* DONE A WRITE IN statement or interleave ($) operation
                 * has produced value requirEing over 32 bits to represent. */
            public const string E533 = "E533 YOU WANT MAYBE WE SHOULD IMPLEMENT 64-BIT VARIABLES?";
            /* Insufficient data. (raised by reading past EOF) */
            public const string E562 = "E562 I DO NOT COMPUTE";
            /* Input data is invalid. */
            public const string E579 = "E579 WHAT BASE AND/OR LANGUAGE INCLUDES \"{0}\" ???";
            /* DONE The expression of a RESUME statement evaluated to #0. */
            public const string E621 = "E621 ERROR TYPE 621 ENCOUNTERED";
            /* NOT DONE Program execution terminated via a RESUME statement instead of GIVE UP. */
            public const string E632 = "E632 THE NEXT STACK RUPTURES.  ALL DIE.  OH, THE EMBARRASSMENT!";
            /* DONE Execution has passed beyond the last statement of the program. */
            public const string E633 = "E633 PROGRAM FELL OFF THE EDGE ON THE WAY TO THE NEW WORLD\n";
            /* DONE A compiler error has occurred (see section 8.1). */
            public const string E774 = "E774 RANDOM COMPILER BUG";
            /* An unexplainable compiler error has occurred */
            public const string E778 = "E778 UNEXPLAINED COMPILER BUG";

            /*
                 * These errors are unique to INTERCAL.NEXT, except for the ones stolen
                 * from C-INTERCAL
                 */
            /* You tried to use a C-INTERCAL extension with the `traditional' flag on */
            //public const string E111 = "E111 COMMUNIST PLOT DETECTED, COMPILER IS SUICIDING";
            /* Cannot find the magically included system library */
            //public const string E127 = "E127 SAYING 'ABRACADABRA' WITHOUT A MAGIC WAND WON'T DO YOU ANY GOOD ON THE WAY TO THE CLOSET\n";
            /* Out of stash space */
            //public const string E222 = "222 BUMMER, DUDE!";
            /* Too many variables. */
            //public const string E333 = "333 YOU CAN'T HAVE EVERYTHING, WHERE WOULD YOU PUT IT?";
            /* DONE A COME FROM statement references a non-existent line label. */
            public const string E444 = "E444 IT CAME FROM BEYOND SPACE";
            /* More than one COME FROM references the same label. */
            public const string E555 = "E555 FLOW DIAGRAM IS EXCESSIVELY CONNECTED ";
            /* Too many source lines. */
            //public const string E666 = "666 COMPILER HAS INDIGESTION";
            /* DONE No such source file. */
            public const string E777 = "E777 A SOURCE IS A SOURCE, OF COURSE, OF COURSE";
            /* Can't open C output file */
            public const string E888 = "E888 I HAVE NO FILE AND I MUST SCREAM";
            /* Can't open C skeleton file. */
            //public const string E999 = "E999 NO SKELETON IN MY CLOSET, WOE IS ME!";
            /* DONE Source file name with invalid extension (use .i or .[3-7]i). */
            public const string E998 = "E998 EXCUSE ME, YOU MUST HAVE ME CONFUSED WITH SOME OTHER COMPILER";
            /* Illegal possession of a controlled unary operator. */
            //public const string E997 = "E997 ILLEGAL POSSESSION OF A CONTROLLED UNARY OPERATOR.";


            //The following error messages are specific to SICK

            /*DONE user specified /t: with something other than exe or library */
            public const string E2001 = "E2001 DON'T GET MUCH CALL FOR THOSE ROUND THESE PARTS";
            /*DONE unable to open as assembly passed with /r (or unable to load assembly at run time) */
            public const string E2002 = "E2002 SOME ASSEMBLY REQUIRED";
            /*DONE Something went wrong when shelling out to csc (csc.exe probably not on PATH)*/
            public const string E2003 = "E2003 C-SHARP OR B-FLAT";
            /*An extension function referenced with /r had the wrong prototype*/
            public const string E2004 = "E2004 SQUARE PEG, ROUND HOLE\nON THE WAY TO {0}.{1}";

        }

        //Intercal libraries use this assembly attribute to 
        //route calls to functions.  The intended usage is this:
        //[assembly: EntryPoint("(3000)", "Class", "method")]
        //In this case the function Class.method will be called
        //whenever a module containing "DO (3000) NEXT" links 
        //to the library in question.  Class.method can be static
        //or instance and can take one of two forms:
        //
        //public void foobar(ExecutionContext ctx)
        // or:
        //public void Method(ExecutionContext ctx, string Label)
        //
        //See NextStatement::Emit() for more details.
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
        public class EntryPointAttribute : Attribute
        {
            public string Label;      //May not be wildcarded (too much room for ambiguity)
            public string ClassName;  //class name
            public string MethodName; //This method must be of type IntercalExtensionDelegate

            public EntryPointAttribute(string Label, string ClassName, string MethodName)
            {
                this.Label = Label;
                this.ClassName = ClassName;
                this.MethodName = MethodName;
            }
        }


        [Serializable]
        public class IntercalException : Exception
        {
            public IntercalException() { }
            public IntercalException(string message) : base(message) { }
            public IntercalException(string message, Exception inner) : base(message, inner) { }
            protected IntercalException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

        ////IExecutionContext holds shared variables used to call across components.
        ////INTERCAL uses an interface so that other languages can define their own
        ////implementation of this interface and pass it in to the DO functions.  This
        ////allows them to hook variable manipulation and implement a variable store
        ////however they like.
        //public interface IExecutionContext
        //{
        //    //These are accessors for variables - one for non-arrays and 
        //    //the other for arrays.
        //    uint this[string varname] { get; set; }
        //    uint this[string varname, int[] indices] { get; set; }

        //    //LastIn and LastOut track the input and output tape positions
        //    //per the Turing Text model.
        //    uint LastIn { get; }
        //    uint LastOut { get; }
        //    void AdvanceInput(uint delta);
        //    void AdvanceOutput(uint delta);
        //    void ReadOut(string s);
        //    void WriteIn(string s);

        //    //These are mostly helper functions. IsArray should be moved to
        //    //INTERCAL.Runtime.Lib, but the rest basically just implement stash/retrieve
        //    //and ignore/remember.
        //    void ReDim(string var, int[] dimensions);
        //    void Stash(string var);
        //    void Retrieve(string var);
        //    void Ignore(string Label);
        //    void Remember(string Label);
        //}

        [Serializable]
        public class ExecutionContext : AsyncDispatcher,
            ILogicalThreadAffinative
        {
            #region Fields and constuctors

            public static ExecutionContext CreateExecutionContext()
            {
                return new ExecutionContext();
            }

            public ExecutionContext()
            {
                Input = Console.OpenStandardInput();
                Output = Console.OpenStandardOutput();
 
                TextOut = new StreamWriter(this.Output);
                BinaryOut = new BinaryReader(this.Input);
                TextIn = new StreamReader(Input);
            }

            //Text I/O is done in INTERCAL 
            //by attaching streams.  By default input and output come from 
            //the console but programs are free to change that if they wish.

            public Stream Input { get; set; }
            public Stream Output { get; private set; }

            public TextReader TextIn { get; private set; } 
            public TextWriter TextOut { get; private set; }
            public BinaryReader BinaryOut { get; private set; }

            //The Turing text model is not very
            //component friendly because whatever you write out is dependent on
            //what the *last* guy did. In order for components to be able to share
            //strings (to do string manipulation) LastIn and LastOut MUST be stored
            //in the execution context.  Furthermore - there has to be some way
            //to query it.

            public uint LastIn { get; private set; }
            public uint LastOut { get; private set; }

            public void AdvanceInput(uint delta) { LastIn = LastIn + delta % 255; }
            public void AdvanceOutput(uint delta) { LastOut = LastOut + delta % 255; }

            public uint this[string varname]
            {
                get
                {
                    if (!Variables.ContainsKey(varname))
                        throw new IntercalException(Messages.E200 + " (" + varname + ")");

                    Variable v = this.GetVariable(varname);

                    if (v is IntVariable)
                        return (v as IntVariable).Value;
                    else
                        Lib.Fail(Messages.E241);
                    //This will never execute - Fail() always throws an exception
                    return 0;
                }
                set
                {
                    Variable v = this.GetVariable(varname);

                    if (v is IntVariable)
                    {
                        if ((v.Name[0] == '.') && value > (UInt32)UInt16.MaxValue)
                            Lib.Fail(Messages.E275);

                        (v as IntVariable).Value = value;
                    }
                    else
                        Lib.Fail(Messages.E241);

                }

            }

            public uint this[string varname, int[] indices]
            {
                get
                {
                    Variable v = this.GetVariable(varname);

                    if (v is ArrayVariable)
                    {
                        ArrayVariable av = v as ArrayVariable;

                        return av[varname, indices];
                    }
                    else
                        Lib.Fail(Messages.E241);

                    //This will never execute - Fail() always throws an exception
                    return 0;
                }
                set
                {
                    Variable v = this.GetVariable(varname);
                    if (v is ArrayVariable)
                    {
                        ArrayVariable av = v as ArrayVariable;

                        av[varname, indices] = value;
                    }
                    else
                        Lib.Fail(Messages.E241);

                }
            }

            [Serializable]
            abstract class Variable
            {
                protected ExecutionContext owner;
                public string Name;
                public bool Enabled = true;

                public Variable(ExecutionContext ctx, string name)
                {
                    this.Name = name;
                    owner = ctx;
                }

                public abstract void Stash();
                public abstract void Retrieve();
            }

            //Variables are always shared across components, just like they 
            //were in the traditional public library.
            //Spot (.) and Two-spot (:) variables are both stored as IntVariables
            [Serializable]
            class IntVariable : Variable
            {
                static Random random = new System.Random();
                public uint val = 0;

                //each variable has it's own little stack for stashing/retrieving values...
                protected Stack<uint> StashStack = new Stack<uint>();

                public IntVariable(ExecutionContext ctx, string name) : base(ctx, name)
                {
                    this.val = 0;
                }

                public uint Value
                {
                    get { return val; }
                    set { if (Enabled) val = value; }
                }

                public override void Stash() { StashStack.Push(val); }
                public override void Retrieve()
                {
                    try
                    {
                        val = (uint)StashStack.Pop();
                    }
                    catch
                    {
                        Lib.Fail(Messages.E436);
                    }
                }
                public override string ToString()
                {
                    return Value.ToString();
                }
            };

            [Serializable]
            class ArrayVariable : Variable
            {
                Array values;

                //each variable has it's own little stack for stashing/retrieving values...
                protected Stack<Array> StashStack = new Stack<Array>();

                public ArrayVariable(ExecutionContext ctx, string name) : base(ctx, name) { }

                public void ReDim(int[] subscripts)
                {
                    int[] lbounds = new int[subscripts.Length];

                    for (int i = 0; i < lbounds.Length; i++)
                    {
                        lbounds[i] = 1;
                    }
                    values = Array.CreateInstance(typeof(uint), subscripts, lbounds);
                    values.SetValue(new System.UInt32(), subscripts);
                }


                public uint this[string var, int[] indices]
                {
                    get
                    {
                        try
                        { return (uint)values.GetValue(indices); }
                        catch
                        {
                            Lib.Fail(Messages.E241);
                            return 0;
                        }
                    }
                    set
                    {
                        try
                        {
                            values.SetValue(value, indices);
                        }
                        catch (Exception e)
                        {
                            Console.Write(e.Message + "var=\"" + this.Name + "\" val=\"" + value.ToString() + "\" indices={");
                            foreach (int i in indices)
                                Console.Write(i);
                            Console.WriteLine("}");
                            Lib.Fail(Messages.E241);
                        }
                    }
                }

                public int Rank { get { return values.Rank; } }
                public int GetLowerBound(int dim) { return values.GetLowerBound(dim); }
                public int GetUpperBound(int dim) { return values.GetUpperBound(dim); }
                public override void Stash()
                {
                    //what to do if a program stashes an unitialized array?  Donald Knuth's
                    //tpk.i depends on this not crashing the runtime.  Knuth is more important
                    //than you or I so this runtime bends to his wishes. This does mean that
                    //it is possible to RETRIEVE a null array.
                    if (values != null)
                        StashStack.Push(values.Clone() as Array);
                    else
                        StashStack.Push(null);
                }

                public override void Retrieve()
                {
                    if (StashStack.Count > 0)
                    {
                        values = StashStack.Pop();
                    }
                    else
                    {
                        Lib.Fail(Messages.E436);
                    }
                }
                public override string ToString()
                {
                    StringBuilder sb = new StringBuilder();

                    //Console.WriteLine("ArrayVariable {0} has {1} values", Name, values.Length);
                    int[] idx = new int[1];

                    foreach (uint v in values)
                    {
                        uint c = owner.LastOut - v;

                        owner.LastOut = c;

                        c = (c & 0x0f) << 4 | (c & 0xf0) >> 4;
                        c = (c & 0x33) << 2 | (c & 0xcc) >> 2;
                        c = (c & 0x55) << 1 | (c & 0xaa) >> 1;

                        sb.Append((char)c);
                    }

                    //Console.WriteLine("Array {0} ToString() ==> {1}", this.Name, sb.ToString());
                    return sb.ToString();
                }
            }

            //This dictionary maps simple identifiers to their values.  All non-array values are 
            //stored here.  Entries in arrays are stored in the Arrays hash table below.
            Dictionary<string, Variable> Variables = new Dictionary<string, Variable>();

            #endregion

            #region control flow
            public void Run(IntercalThreadProc proc)
            {
                StartProc sp = Evaluate;
                sp.BeginInvoke(proc, 0, ar => sp.EndInvoke(ar), null);

                lock (SyncLock)
                {
                    while (!Done)
                    {
                        Monitor.Wait(SyncLock);
                    }
                }

                if (CurrentException != null)
                {
                    throw CurrentException;
                }
            }
            public bool Evaluate(IntercalThreadProc proc, int label)
            {
                var frame = new ExecutionFrame(this, proc, label);

                lock (SyncLock)
                {
                    NextingStack.Push(frame);
                }

                bool result = frame.Start();
                return result;
            }

            #endregion

            #region STASH/RETRIEVE

            //STASH / RETRIEVE always operate on the global execution context - all
            //variables have visibility to everyone in the program flow.  Note that
            //there is no way to know if any given identifier is currently holding a value
            //set by another component or is just uninitialized.  Such is the power 
            //of intercal!  Perhaps every module should track in its metadata a listing
            //of the identifiers used in that component?  These would take the form
            //of assembly attributes?
            Variable GetVariable(string varname)
            {
                Variable retval = null;

                if (varname[0] == '.' || varname[0] == ':')
                {
                    if (!Variables.TryGetValue(varname, out retval))
                    {
                        Variable v = new IntVariable(this, varname);
                        Variables[varname] = v;
                        retval = v;
                    }
                }
                else if (varname[0] == ',' || varname[0] == ';')
                {

                    if (!Variables.TryGetValue(varname, out retval))
                    {
                        Variable v = new ArrayVariable(this, varname);
                        Variables[varname] = v;
                        retval = v;
                    }
                }
                else
                {
                    Lib.Fail(Messages.E241);
                }

                return retval;
            }


            //Is there any reason we can't just use native array classes? Actually,
            //yes.  The execution engine holds onto the variables because of 
            //Stash / Retrieve.  Hmm, is that convincing? Would there be 
            //harm in just giving clients an object reference? (which would
            //support stashing / retrieving)?
            public void ReDim(string var, int[] dimensions)
            {
                ArrayVariable v = GetVariable(var) as ArrayVariable;
                if (v != null)
                    v.ReDim(dimensions);
                else
                    Lib.Fail(Messages.E241);
            }

            public void Stash(string var)
            {
                GetVariable(var).Stash();
            }

            public void Retrieve(string var)
            {
                GetVariable(var).Retrieve();
            }
            #endregion

            #region IGNORE/REMEMBER
            //IGNORE / REMEMBER are global because variables are visible everywhere. If 
            //module A Ignores a variable and passes it to B any assigns that B makes
            //will be ignored.  This means B can ignore and return back to A and A has
            //no good way to even determing if any given variable is currently ignored.
            public void Ignore(string Label)
            {
                this.GetVariable(Label).Enabled = false;
            }

            public void Remember(string Label)
            {
                this.GetVariable(Label).Enabled = true;
            }
            #endregion

            #region READ/WRITE
            //The execution context exposes two public properties (an input stream and
            //an output stream). Programs hosting intercal components can do string communication
            //by hooking the output stream and calling routines that do a DO READ OUT. 

            //String manipulation is impossible.  Suppose an INTERCAL module calls a C# module, and 
            //the C# module wants to do string manipulation on the string stored in ;0.  In order
            //to decipher the characters in the array it will be necessary for the C# module to
            //where the input tape was positioned when the characters were read in (since strings
            //are stored as deltas rather than absolute values).  For example, if the array contains
            //{ 65, 1, 1, 1} and LastIn is 68 then you could ostensibly conclude that the string
            //contains {A B C D}, but this is only true if the array was the last one written to.
            //In keeping with the spirit of the Turing Text model I think the context 
            //should save the current input tape position whenever a WRITE IN is encountered,
            //e.g. (0) {65,1,1,1} is enough information to recover "ABCD".
            //Existing programs continue to work; new components can peek at the value if they want
            //to do string manipulation.  Hopefully we can make this completely transparent
            //to modules written in INTERCAL.  

            //As of right now I haven't done anything yet to enable this.
            public void ReadOut(string identifier)
            {
                Trace.WriteLine(string.Format("Reading out '{0}'", identifier.Length));

                var next = Variables[identifier].ToString();
                Trace.WriteLine(string.Format("Reading out {0}", next));

                if (Variables[identifier] is ArrayVariable)
                    TextOut.Write(next);
                else
                    TextOut.WriteLine(next);

                TextOut.Flush();
            }

            public void WriteIn(string identifier)
            {
                Trace.WriteLine(string.Format("Writing into {0}", identifier));
                //the intercal model is stream-based - calling WriteIn reads as
                //many chars as there are in the array (or fewer if EOF is reached)
                //Console.Write("{0}?>", s);

                int[] idx = new int[1];

                if ((identifier[0] == ',') || (identifier[0] == ';'))
                {
                    ArrayVariable av = this.Variables[identifier] as ArrayVariable;
                    if (av.Rank != 1)
                        throw new IntercalException(Messages.E241);

                    for (int i = av.GetLowerBound(0); i <= av.GetUpperBound(0); i++)
                    {
                        idx[0] = i;

                        uint c = (uint)BinaryOut.ReadChar();

                        uint v = (c - this.LastIn) % 256;
                        this.LastIn = c;

                        Trace.WriteLine(string.Format("Writing '{0}' into index {1}", (char)c, i));
                        this[identifier, idx] = v;
                    }
                }
                else
                {
                        string input = TextIn.ReadLine();
                        try
                        {
                            //Note that this compiler today only works in wimpmode.  To do it
                            //right we will need to have satellite assemblies, one for each of
                            //many different languages.
                            this[identifier] = UInt32.Parse(input);
                        }
                        catch
                        {
                            Lib.Fail(String.Format(Messages.E579, input));
                        }
                 }
            }

            #endregion
        }

        //This class provides basic bit-mangling functionality, e.g.
        //uint u = Bits.Mingle(0, 65535);
        public class Lib
        {
            static Random random = new Random();

            static uint[] bitflags =
        {
            0x00000001,         0x00000002,         0x00000004,         0x00000008,
            0x00000010,         0x00000020,         0x00000040,         0x00000080,
            0x00000100,         0x00000200,         0x00000400,         0x00000800,
            0x00001000,         0x00002000,         0x00004000,         0x00008000,
            0x00010000,         0x00020000,         0x00040000,         0x00080000,
            0x00100000,         0x00200000,         0x00400000,         0x00800000,
            0x01000000,         0x02000000,         0x04000000,         0x08000000,
            0x10000000,         0x20000000,         0x40000000,         0x80000000
        };

            public static uint Mingle(uint men, uint ladies)
            {
                ushort a = (ushort)men;
                ushort b = (ushort)ladies;

                //mingle takes two 16 bit values andbuilds a 32-bit operator by "mingling" their bits
                //if ((a > UInt16.MaxValue) || (b > UInt16.MaxValue))
                //    throw new IntercalException(Messages.E533);

                uint retval = 0;

                for (int i = 15; i >= 0; i--)
                {
                    if ((a & (ushort)bitflags[i]) != 0)
                        retval |= bitflags[2 * i + 1];

                    if ((b & (ushort)bitflags[i]) != 0)
                        retval |= bitflags[2 * i];
                }

                return retval;
            }

            public static uint Select(uint a, uint b)
            {
                uint retval = 0;
                int bit = 0;

                for (int i = 0; i < 32; i++)
                {
                    if ((b & bitflags[i]) != 0)
                    {
                        if ((a & bitflags[i]) != 0)
                            retval |= bitflags[bit];
                        bit++;
                    }
                }

                return retval;
            }

            public static ushort Select(ushort a, ushort b)
            {
                ushort retval = 0;
                int bit = 0;

                for (int i = 0; i < 16; i++)
                {
                    if ((b & bitflags[i]) != 0)
                    {
                        if ((a & bitflags[i]) != 0)
                            retval |= (ushort)bitflags[bit];
                        bit++;
                    }
                }

                return retval;
            }

            public static uint Rotate(uint val)
            {
                bool b = ((val & bitflags[0]) != 0);
                val /= 2;
                if (b)
                    val |= bitflags[31];
                return val;
            }

            public static ushort Rotate(ushort val)
            {
                bool b = ((val & bitflags[0]) != 0);
                val /= 2;
                if (b)
                    val |= (ushort)0x8000;
                return val;
            }

            public static ushort Reverse(ushort val)
            {
                ushort retval = 0;
                for (int i = 0; i < 16; i++)
                {
                    if ((val & bitflags[i]) != 0)
                        retval |= (ushort)bitflags[15 - i];
                }
                return retval;
            }

            public static uint And(uint val)
            {
                if (val < UInt16.MaxValue)
                    return (uint)UnaryAnd16((ushort)val);
                else
                    return UnaryAnd32(val);
            }
            public static uint UnaryAnd32(uint val) { return val & Rotate(val); }
            public static ushort UnaryAnd16(ushort val) { return (ushort)(val & Rotate(val)); }

            public static uint Or(uint val)
            {
                if (val < UInt16.MaxValue)
                    return (uint)UnaryOr16((ushort)val);
                else
                    return UnaryOr32(val);
            }
            public static uint UnaryOr32(uint val) { return val | Rotate(val); }
            public static ushort UnaryOr16(ushort val) { return (ushort)(val | Rotate(val)); }

            public static uint Xor(uint val)
            {
                if (val < UInt16.MaxValue)
                    return (uint)UnaryXor16((ushort)val);
                else
                    return UnaryXor32(val);
            }
            public static uint UnaryXor32(uint val) { return val ^ Rotate(val); }
            public static ushort UnaryXor16(ushort val) { return (ushort)(val ^ Rotate(val)); }


            public static int Rand(int n)
            {
                return random.Next(n);
            }

            //Call this to raise an exception. This really should
            //be a method on the execution context, not in the 
            //utility library
            public static void Fail(string errcode)
            {
                throw new IntercalException(errcode);
            }
        }
    }
}