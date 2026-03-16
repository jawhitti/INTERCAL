using System;
using INTERCAL.Runtime;
using System.Diagnostics;
[Serializable]
public class primes : System.Object
{ 
   public void Run(){
      ExecutionContext ec = INTERCAL.Runtime.ExecutionContext.CreateExecutionContext();
      ec.Run(Eval);
   }

   bool[] abstainMap = new bool[] {true};

   System.Collections.Generic.Stack<int> _nextStack = new System.Collections.Generic.Stack<int>();
   int _forgetAdj = 0;

   protected void Eval(ExecutionFrame frame)   {
   switch(frame.Label)
   {
      case 15L: _nextStack.Push(0); goto label_15;
      case 13L: _nextStack.Push(0); goto label_13;
      case 16L: _nextStack.Push(0); goto label_16;
      case 12L: _nextStack.Push(0); goto label_12;
      case 14L: _nextStack.Push(0); goto label_14;
      case 11L: _nextStack.Push(0); goto label_11;
      case 23L: _nextStack.Push(0); goto label_23;
      case 22L: _nextStack.Push(0); goto label_22;
      case 21L: _nextStack.Push(0); goto label_21;
      case 2010L: _nextStack.Push(0); goto label_2010;
      case 2000L: _nextStack.Push(0); goto label_2000;
      case 2001L: _nextStack.Push(0); goto label_2001;
      case 2003L: _nextStack.Push(0); goto label_2003;
      case 2002L: _nextStack.Push(0); goto label_2002;
      case 2004L: _nextStack.Push(0); goto label_2004;
      case 2020L: _nextStack.Push(0); goto label_2020;
      case 2030L: _nextStack.Push(0); goto label_2030;
      case 2033L: _nextStack.Push(0); goto label_2033;
      case 2032L: _nextStack.Push(0); goto label_2032;
      case 2036L: _nextStack.Push(0); goto label_2036;
      case 2034L: _nextStack.Push(0); goto label_2034;
      case 2035L: _nextStack.Push(0); goto label_2035;
      case 2031L: _nextStack.Push(0); goto label_2031;
   }
   ulong dot_1 = 0;
   ulong dot_10 = 0;
   ulong dot_11 = 0;
   ulong dot_12 = 0;
   ulong dot_13 = 0;
   ulong dot_2 = 0;
   ulong dot_3 = 0;
   ulong dot_4 = 0;
   ulong dot_5 = 0;


/* DO .10 <- #1*/
#line hidden
Trace.WriteLine("[0000] CalculateStatement");
#line 1 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .10 <- {0}",1));
frame.ExecutionContext[".10"] = 1;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE COME FROM (23)*/
#line hidden

line_1:
Trace.WriteLine("[0001] ComeFromStatement");
#line 2 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .11 <- '.10$#1'~'#32767$#1'*/
#line hidden
Trace.WriteLine("[0002] CalculateStatement");
#line 3 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .11 <- {0}",(ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".10"], 1))),(uint)(715827883))));
frame.ExecutionContext[".11"] = (ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".10"], 1))),(uint)(715827883));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .12 <- #1*/
#line hidden
Trace.WriteLine("[0003] CalculateStatement");
#line 4 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .12 <- {0}",1));
frame.ExecutionContext[".12"] = 1;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE COME FROM (16)*/
#line hidden

line_4:
Trace.WriteLine("[0004] ComeFromStatement");
#line 5 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .13 <- '.12$#1'~'#32767$#1'*/
#line hidden
Trace.WriteLine("[0005] CalculateStatement");
#line 6 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .13 <- {0}",(ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".12"], 1))),(uint)(715827883))));
frame.ExecutionContext[".13"] = (ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".12"], 1))),(uint)(715827883));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .1 <- .11*/
#line hidden
Trace.WriteLine("[0006] CalculateStatement");
#line 7 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .1 <- {0}",frame.ExecutionContext[".11"]));
frame.ExecutionContext[".1"] = frame.ExecutionContext[".11"];

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .2 <- .13*/
#line hidden
Trace.WriteLine("[0007] CalculateStatement");
#line 8 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .2 <- {0}",frame.ExecutionContext[".13"]));
frame.ExecutionContext[".2"] = frame.ExecutionContext[".13"];

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (2030) NEXT*/
#line hidden
Trace.WriteLine("[0008] NextStatement");
#line 9 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(1);
goto label_2030;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_1: ;
if (frame.ExecutionContext.Done) goto exit;

/* DO (11) NEXT*/
#line hidden
Trace.WriteLine("[0009] NextStatement");
#line 10 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(2);
goto label_11;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_2: ;
if (frame.ExecutionContext.Done) goto exit;

/* (15)	DO (13) NEXT*/
#line hidden

label_15: Console.Error.Write("15 ");
Trace.WriteLine("[0010] NextStatement");
#line 11 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(3);
goto label_13;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
    goto line_23;
_ret_3: ;
if (frame.ExecutionContext.Done) goto exit;

/* (13)	DO .3 <- "?'.4~.4'$#2"~#3*/
#line hidden

label_13: Console.Error.Write("13 ");
Trace.WriteLine("[0011] CalculateStatement");
#line 12 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .3 <- {0}",(ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(frame.ExecutionContext[".4"]),(uint)(frame.ExecutionContext[".4"]))), 2)))),(uint)(3))));
frame.ExecutionContext[".3"] = (ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(frame.ExecutionContext[".4"]),(uint)(frame.ExecutionContext[".4"]))), 2)))),(uint)(3));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (14) NEXT*/
#line hidden
Trace.WriteLine("[0012] NextStatement");
#line 13 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(4);
goto label_14;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_4: ;
if (frame.ExecutionContext.Done) goto exit;

/* PLEASE FORGET #1*/
#line hidden
Trace.WriteLine("[0013] ForgetStatement");
#line 14 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Forgetting 1");
{ int _n = (int)(1); for (int _i = 0; _i < _n && _nextStack.Count > 0; _i++) _nextStack.Pop(); }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .1 <- .12*/
#line hidden
Trace.WriteLine("[0014] CalculateStatement");
#line 15 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .1 <- {0}",frame.ExecutionContext[".12"]));
frame.ExecutionContext[".1"] = frame.ExecutionContext[".12"];

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (1020) NEXT*/
#line hidden
Trace.WriteLine("[0015] NextStatement");
#line 16 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Doing (1020) Next");;
{
   bool shouldTerminate = fullsyslibProp.DO_1020(frame.ExecutionContext);
   if (shouldTerminate) goto exit;
   int _rd = frame.ExecutionContext.ResumeDepth;
   frame.ExecutionContext.ResumeDepth = 0;
   if (_rd > 0) {
      int _retLabel = 0;
      int _popped = 0;
      while (_popped < _rd && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
      int _remaining = _rd - _popped;
      if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
      frame.ExecutionContext.ResumeDepth = _remaining;
      goto exit;
   }
}

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (16)	DO .12 <- .1*/
#line hidden

label_16: Console.Error.Write("16 ");
Trace.WriteLine("[0016] CalculateStatement");
#line 17 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .12 <- {0}",frame.ExecutionContext[".1"]));
frame.ExecutionContext[".12"] = frame.ExecutionContext[".1"];

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
    goto line_4;

/* (12)	DO .3 <- '?.2$.3'~'#0$#65535'*/
#line hidden

label_12: Console.Error.Write("12 ");
Trace.WriteLine("[0017] CalculateStatement");
#line 18 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .3 <- {0}",(ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(frame.ExecutionContext[".2"], frame.ExecutionContext[".3"])))),(uint)(1431655765))));
frame.ExecutionContext[".3"] = (ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(frame.ExecutionContext[".2"], frame.ExecutionContext[".3"])))),(uint)(1431655765));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .3 <- '?"'&"'.2~.3'~'"?'?.3~.3'$#32768"~"#0$#65535"'"$
                 ".3~.3"'~#1"$#2'~#3*/
#line hidden
Trace.WriteLine("[0018] CalculateStatement");
#line 19 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .3 <- {0}",(ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)((Lib.And(Lib.Mingle(((ulong)Lib.Select((uint)(((ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(frame.ExecutionContext[".3"])))),(uint)(((ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle((Lib.Xor((ulong)Lib.Select((uint)(frame.ExecutionContext[".3"]),(uint)(frame.ExecutionContext[".3"])))), 32768)))),(uint)(1431655765)))))), ((ulong)Lib.Select((uint)(frame.ExecutionContext[".3"]),(uint)(frame.ExecutionContext[".3"]))))))),(uint)(1))), 2)))),(uint)(3))));
frame.ExecutionContext[".3"] = (ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)((Lib.And(Lib.Mingle(((ulong)Lib.Select((uint)(((ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(frame.ExecutionContext[".3"])))),(uint)(((ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle((Lib.Xor((ulong)Lib.Select((uint)(frame.ExecutionContext[".3"]),(uint)(frame.ExecutionContext[".3"])))), 32768)))),(uint)(1431655765)))))), ((ulong)Lib.Select((uint)(frame.ExecutionContext[".3"]),(uint)(frame.ExecutionContext[".3"]))))))),(uint)(1))), 2)))),(uint)(3));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (14)	PLEASE RESUME .3*/
#line hidden

label_14: Console.Error.Write("14 ");
Trace.WriteLine("[0019] ResumeStatement");
#line 21 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
;
#line hidden
   {
      int depth = (int)(frame.ExecutionContext[".3"]);
      if (depth > 0) {
         int _retLabel = 0;
         int _popped = 0;
         while (_popped < depth && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
         int _remaining = depth - _popped;
         if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
         frame.ExecutionContext.ResumeDepth = _remaining;
         goto exit;
      }
   }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (11)	DO (12) NEXT*/
#line hidden

label_11: Console.Error.Write("11 ");
Trace.WriteLine("[0020] NextStatement");
#line 22 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(5);
goto label_12;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_5: ;
if (frame.ExecutionContext.Done) goto exit;

/* DO FORGET #1*/
#line hidden
Trace.WriteLine("[0021] ForgetStatement");
#line 23 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Forgetting 1");
{ int _n = (int)(1); for (int _i = 0; _i < _n && _nextStack.Count > 0; _i++) _nextStack.Pop(); }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE READ OUT .11*/
#line hidden
Trace.WriteLine("[0022] ReadOutStatement");
#line 24 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
frame.ExecutionContext.ReadOut(frame.ExecutionContext[".11"]);
#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO COME FROM (15)*/
#line hidden

line_23:
Trace.WriteLine("[0023] ComeFromStatement");
#line 25 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .1 <- .10*/
#line hidden
Trace.WriteLine("[0024] CalculateStatement");
#line 26 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .1 <- {0}",frame.ExecutionContext[".10"]));
frame.ExecutionContext[".1"] = frame.ExecutionContext[".10"];

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (1020) NEXT*/
#line hidden
Trace.WriteLine("[0025] NextStatement");
#line 27 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Doing (1020) Next");;
{
   bool shouldTerminate = fullsyslibProp.DO_1020(frame.ExecutionContext);
   if (shouldTerminate) goto exit;
   int _rd = frame.ExecutionContext.ResumeDepth;
   frame.ExecutionContext.ResumeDepth = 0;
   if (_rd > 0) {
      int _retLabel = 0;
      int _popped = 0;
      while (_popped < _rd && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
      int _remaining = _rd - _popped;
      if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
      frame.ExecutionContext.ResumeDepth = _remaining;
      goto exit;
   }
}

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .10 <- .1*/
#line hidden
Trace.WriteLine("[0026] CalculateStatement");
#line 28 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .10 <- {0}",frame.ExecutionContext[".1"]));
frame.ExecutionContext[".10"] = frame.ExecutionContext[".1"];

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (23)	DO (21) NEXT*/
#line hidden

label_23: Console.Error.Write("23 ");
Trace.WriteLine("[0027] NextStatement");
#line 29 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(6);
goto label_21;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
    goto line_1;
_ret_6: ;
if (frame.ExecutionContext.Done) goto exit;

/* (22)	PLEASE RESUME "?'.10~#32768'$#2"~#3*/
#line hidden

label_22: Console.Error.Write("22 ");
Trace.WriteLine("[0028] ResumeStatement");
#line 30 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
;
#line hidden
   {
      int depth = (int)((ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(frame.ExecutionContext[".10"]),(uint)(32768))), 2)))),(uint)(3)));
      if (depth > 0) {
         int _retLabel = 0;
         int _popped = 0;
         while (_popped < depth && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
         int _remaining = depth - _popped;
         if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
         frame.ExecutionContext.ResumeDepth = _remaining;
         goto exit;
      }
   }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (21)	DO (22) NEXT*/
#line hidden

label_21: Console.Error.Write("21 ");
Trace.WriteLine("[0029] NextStatement");
#line 31 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(7);
goto label_22;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_7: ;
if (frame.ExecutionContext.Done) goto exit;

/* DO FORGET #1*/
#line hidden
Trace.WriteLine("[0030] ForgetStatement");
#line 32 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Forgetting 1");
{ int _n = (int)(1); for (int _i = 0; _i < _n && _nextStack.Count > 0; _i++) _nextStack.Pop(); }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE GIVE UP*/
#line hidden
Trace.WriteLine("[0031] GiveUpStatement");
#line 33 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
           frame.ExecutionContext.GiveUp();
           goto exit;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (2010)  PLEASE ABSTAIN FROM (2004)*/
#line hidden

label_2010: Console.Error.Write("2010 ");
Trace.WriteLine("[0032] AbstainStatement");
#line 35 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
abstainMap[0] = false;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (2000)  PLEASE STASH .2*/
#line hidden

label_2000: Console.Error.Write("2000 ");
Trace.WriteLine("[0033] StashStatement");
#line 36 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Stashing .2");;
frame.ExecutionContext.Stash(".2");

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .2 <- #1*/
#line hidden
Trace.WriteLine("[0034] CalculateStatement");
#line 37 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .2 <- {0}",1));
frame.ExecutionContext[".2"] = 1;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (2001) NEXT*/
#line hidden
Trace.WriteLine("[0035] NextStatement");
#line 38 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(8);
goto label_2001;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_8: ;
if (frame.ExecutionContext.Done) goto exit;

/* (2001)  PLEASE FORGET #1*/
#line hidden

label_2001: Console.Error.Write("2001 ");
Trace.WriteLine("[0036] ForgetStatement");
#line 39 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Forgetting 1");
{ int _n = (int)(1); for (int _i = 0; _i < _n && _nextStack.Count > 0; _i++) _nextStack.Pop(); }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .1 <- '?.1$.2'~'#0$#65535'*/
#line hidden
Trace.WriteLine("[0037] CalculateStatement");
#line 40 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .1 <- {0}",(ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(frame.ExecutionContext[".1"], frame.ExecutionContext[".2"])))),(uint)(1431655765))));
frame.ExecutionContext[".1"] = (ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(frame.ExecutionContext[".1"], frame.ExecutionContext[".2"])))),(uint)(1431655765));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (2002) NEXT*/
#line hidden
Trace.WriteLine("[0038] NextStatement");
#line 41 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(9);
goto label_2002;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_9: ;
if (frame.ExecutionContext.Done) goto exit;

/* DO .2 <- '.2$#0'~'#32767$#1'*/
#line hidden
Trace.WriteLine("[0039] CalculateStatement");
#line 42 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .2 <- {0}",(ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".2"], 0))),(uint)(715827883))));
frame.ExecutionContext[".2"] = (ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".2"], 0))),(uint)(715827883));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (2001) NEXT*/
#line hidden
Trace.WriteLine("[0040] NextStatement");
#line 43 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(10);
goto label_2001;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_10: ;
if (frame.ExecutionContext.Done) goto exit;

/* (2003)  PLEASE RESUME "?'.1~.2'$#1"~#3*/
#line hidden

label_2003: Console.Error.Write("2003 ");
Trace.WriteLine("[0041] ResumeStatement");
#line 44 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
;
#line hidden
   {
      int depth = (int)((ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(frame.ExecutionContext[".1"]),(uint)(frame.ExecutionContext[".2"]))), 1)))),(uint)(3)));
      if (depth > 0) {
         int _retLabel = 0;
         int _popped = 0;
         while (_popped < depth && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
         int _remaining = depth - _popped;
         if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
         frame.ExecutionContext.ResumeDepth = _remaining;
         goto exit;
      }
   }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (2002)  DO (2003) NEXT*/
#line hidden

label_2002: Console.Error.Write("2002 ");
Trace.WriteLine("[0042] NextStatement");
#line 45 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(11);
goto label_2003;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_11: ;
if (frame.ExecutionContext.Done) goto exit;

/* PLEASE RETRIEVE .2*/
#line hidden
Trace.WriteLine("[0043] RetrieveStatement");
#line 46 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Retrieving .2");;
frame.ExecutionContext.Retrieve(".2");

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (2004)	PLEASE RESUME #2*/
#line hidden

label_2004: Console.Error.Write("2004 ");
if(abstainMap[0])
{
Trace.WriteLine("[0044] ResumeStatement");
#line 47 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
;
#line hidden
   {
      int depth = (int)(2);
      if (depth > 0) {
         int _retLabel = 0;
         int _popped = 0;
         while (_popped < depth && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
         int _remaining = depth - _popped;
         if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
         frame.ExecutionContext.ResumeDepth = _remaining;
         goto exit;
      }
   }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
}


/* PLEASE DO REINSTATE (2004)*/
#line hidden
Trace.WriteLine("[0045] ReinstateStatement");
#line 48 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
abstainMap[0] = true;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE RESUME '?"'.1~.1'~#1"$#2'~#6*/
#line hidden
Trace.WriteLine("[0046] ResumeStatement");
#line 49 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
;
#line hidden
   {
      int depth = (int)((ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(((ulong)Lib.Select((uint)(frame.ExecutionContext[".1"]),(uint)(frame.ExecutionContext[".1"])))),(uint)(1))), 2)))),(uint)(6)));
      if (depth > 0) {
         int _retLabel = 0;
         int _popped = 0;
         while (_popped < depth && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
         int _remaining = depth - _popped;
         if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
         frame.ExecutionContext.ResumeDepth = _remaining;
         goto exit;
      }
   }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (2020)  PLEASE STASH .2 + .3*/
#line hidden

label_2020: Console.Error.Write("2020 ");
Trace.WriteLine("[0047] StashStatement");
#line 50 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Stashing .2");;
frame.ExecutionContext.Stash(".2");
Trace.WriteLine("       Stashing .3");;
frame.ExecutionContext.Stash(".3");

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (1021) NEXT*/
#line hidden
Trace.WriteLine("[0048] NextStatement");
#line 51 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Doing (1021) Next");;
{
   bool shouldTerminate = fullsyslibProp.DO_1021(frame.ExecutionContext);
   if (shouldTerminate) goto exit;
   int _rd = frame.ExecutionContext.ResumeDepth;
   frame.ExecutionContext.ResumeDepth = 0;
   if (_rd > 0) {
      int _retLabel = 0;
      int _popped = 0;
      while (_popped < _rd && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
      int _remaining = _rd - _popped;
      if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
      frame.ExecutionContext.ResumeDepth = _remaining;
      goto exit;
   }
}

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (2030)	DO STASH .1 + .5*/
#line hidden

label_2030: Console.Error.Write("2030 ");
Trace.WriteLine("[0049] StashStatement");
#line 52 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Stashing .1");;
frame.ExecutionContext.Stash(".1");
Trace.WriteLine("       Stashing .5");;
frame.ExecutionContext.Stash(".5");

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .3 <- #0*/
#line hidden
Trace.WriteLine("[0050] CalculateStatement");
#line 53 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .3 <- {0}",0));
frame.ExecutionContext[".3"] = 0;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .5 <- '?"'.2~.2'~#1"$#1'~#3*/
#line hidden
Trace.WriteLine("[0051] CalculateStatement");
#line 54 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .5 <- {0}",(ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(((ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(frame.ExecutionContext[".2"])))),(uint)(1))), 1)))),(uint)(3))));
frame.ExecutionContext[".5"] = (ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(((ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(frame.ExecutionContext[".2"])))),(uint)(1))), 1)))),(uint)(3));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE DO (2031) NEXT*/
#line hidden
Trace.WriteLine("[0052] NextStatement");
#line 55 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(12);
goto label_2031;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_12: ;
if (frame.ExecutionContext.Done) goto exit;

/* DO .4 <- #1*/
#line hidden
Trace.WriteLine("[0053] CalculateStatement");
#line 56 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .4 <- {0}",1));
frame.ExecutionContext[".4"] = 1;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE DO (2033) NEXT*/
#line hidden
Trace.WriteLine("[0054] NextStatement");
#line 57 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(13);
goto label_2033;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_13: ;
if (frame.ExecutionContext.Done) goto exit;

/* (2033)	DO FORGET #1*/
#line hidden

label_2033: Console.Error.Write("2033 ");
Trace.WriteLine("[0055] ForgetStatement");
#line 58 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Forgetting 1");
{ int _n = (int)(1); for (int _i = 0; _i < _n && _nextStack.Count > 0; _i++) _nextStack.Pop(); }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .5 <- '?".2~#32768"$#2'~#3*/
#line hidden
Trace.WriteLine("[0056] CalculateStatement");
#line 59 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .5 <- {0}",(ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(32768))), 2)))),(uint)(3))));
frame.ExecutionContext[".5"] = (ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(32768))), 2)))),(uint)(3));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (2032) NEXT*/
#line hidden
Trace.WriteLine("[0057] NextStatement");
#line 60 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(14);
goto label_2032;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_14: ;
if (frame.ExecutionContext.Done) goto exit;

/* DO .2 <- '.2$#0'~'#32767$#1'*/
#line hidden
Trace.WriteLine("[0058] CalculateStatement");
#line 61 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .2 <- {0}",(ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".2"], 0))),(uint)(715827883))));
frame.ExecutionContext[".2"] = (ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".2"], 0))),(uint)(715827883));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE DO .4 <- '.4$#0'~'#32767$#1'*/
#line hidden
Trace.WriteLine("[0059] CalculateStatement");
#line 62 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .4 <- {0}",(ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".4"], 0))),(uint)(715827883))));
frame.ExecutionContext[".4"] = (ulong)Lib.Select((uint)((Lib.Mingle(frame.ExecutionContext[".4"], 0))),(uint)(715827883));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (2033) NEXT*/
#line hidden
Trace.WriteLine("[0060] NextStatement");
#line 63 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(15);
goto label_2033;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_15: ;
if (frame.ExecutionContext.Done) goto exit;

/* (2032)	DO (1001) NEXT*/
#line hidden

label_2032: Console.Error.Write("2032 ");
Trace.WriteLine("[0061] NextStatement");
#line 64 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Doing (1001) Next");;
{
   bool shouldTerminate = fullsyslibProp.DO_1001(frame.ExecutionContext);
   if (shouldTerminate) goto exit;
   int _rd = frame.ExecutionContext.ResumeDepth;
   frame.ExecutionContext.ResumeDepth = 0;
   if (_rd > 0) {
      int _retLabel = 0;
      int _popped = 0;
      while (_popped < _rd && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
      int _remaining = _rd - _popped;
      if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
      frame.ExecutionContext.ResumeDepth = _remaining;
      goto exit;
   }
}

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (2036)	PLEASE FORGET #1*/
#line hidden

label_2036: Console.Error.Write("2036 ");
Trace.WriteLine("[0062] ForgetStatement");
#line 65 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Forgetting 1");
{ int _n = (int)(1); for (int _i = 0; _i < _n && _nextStack.Count > 0; _i++) _nextStack.Pop(); }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .5 <- '?.1$.2'~'#0$#65535'*/
#line hidden
Trace.WriteLine("[0063] CalculateStatement");
#line 66 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .5 <- {0}",(ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(frame.ExecutionContext[".1"], frame.ExecutionContext[".2"])))),(uint)(1431655765))));
frame.ExecutionContext[".5"] = (ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(frame.ExecutionContext[".1"], frame.ExecutionContext[".2"])))),(uint)(1431655765));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .5 <- '?"'&"'.2~.5'~'"?'?.5~.5'$#32768"~"#0$#65535"'"$
                 ".5~.5"'~#1"$#2'~#3*/
#line hidden
Trace.WriteLine("[0064] CalculateStatement");
#line 67 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .5 <- {0}",(ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)((Lib.And(Lib.Mingle(((ulong)Lib.Select((uint)(((ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(frame.ExecutionContext[".5"])))),(uint)(((ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle((Lib.Xor((ulong)Lib.Select((uint)(frame.ExecutionContext[".5"]),(uint)(frame.ExecutionContext[".5"])))), 32768)))),(uint)(1431655765)))))), ((ulong)Lib.Select((uint)(frame.ExecutionContext[".5"]),(uint)(frame.ExecutionContext[".5"]))))))),(uint)(1))), 2)))),(uint)(3))));
frame.ExecutionContext[".5"] = (ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)((Lib.And(Lib.Mingle(((ulong)Lib.Select((uint)(((ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(frame.ExecutionContext[".5"])))),(uint)(((ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle((Lib.Xor((ulong)Lib.Select((uint)(frame.ExecutionContext[".5"]),(uint)(frame.ExecutionContext[".5"])))), 32768)))),(uint)(1431655765)))))), ((ulong)Lib.Select((uint)(frame.ExecutionContext[".5"]),(uint)(frame.ExecutionContext[".5"]))))))),(uint)(1))), 2)))),(uint)(3));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (2034) NEXT*/
#line hidden
Trace.WriteLine("[0065] NextStatement");
#line 69 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(16);
goto label_2034;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_16: ;
if (frame.ExecutionContext.Done) goto exit;

/* DO .5 <- .3*/
#line hidden
Trace.WriteLine("[0066] CalculateStatement");
#line 70 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .5 <- {0}",frame.ExecutionContext[".3"]));
frame.ExecutionContext[".5"] = frame.ExecutionContext[".3"];

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (1010) NEXT*/
#line hidden
Trace.WriteLine("[0067] NextStatement");
#line 71 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Doing (1010) Next");;
{
   bool shouldTerminate = fullsyslibProp.DO_1010(frame.ExecutionContext);
   if (shouldTerminate) goto exit;
   int _rd = frame.ExecutionContext.ResumeDepth;
   frame.ExecutionContext.ResumeDepth = 0;
   if (_rd > 0) {
      int _retLabel = 0;
      int _popped = 0;
      while (_popped < _rd && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
      int _remaining = _rd - _popped;
      if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
      frame.ExecutionContext.ResumeDepth = _remaining;
      goto exit;
   }
}

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE DO .1 <- .3*/
#line hidden
Trace.WriteLine("[0068] CalculateStatement");
#line 72 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .1 <- {0}",frame.ExecutionContext[".3"]));
frame.ExecutionContext[".1"] = frame.ExecutionContext[".3"];

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .3 <- 'V.4$.5'~'#0$#65535'*/
#line hidden
Trace.WriteLine("[0069] CalculateStatement");
#line 73 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .3 <- {0}",(ulong)Lib.Select((uint)((Lib.Or(Lib.Mingle(frame.ExecutionContext[".4"], frame.ExecutionContext[".5"])))),(uint)(1431655765))));
frame.ExecutionContext[".3"] = (ulong)Lib.Select((uint)((Lib.Or(Lib.Mingle(frame.ExecutionContext[".4"], frame.ExecutionContext[".5"])))),(uint)(1431655765));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (2035) NEXT*/
#line hidden
Trace.WriteLine("[0070] NextStatement");
#line 74 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(17);
goto label_2035;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_17: ;
if (frame.ExecutionContext.Done) goto exit;

/* (2034)	PLEASE DO (1001) NEXT*/
#line hidden

label_2034: Console.Error.Write("2034 ");
Trace.WriteLine("[0071] NextStatement");
#line 75 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Doing (1001) Next");;
{
   bool shouldTerminate = fullsyslibProp.DO_1001(frame.ExecutionContext);
   if (shouldTerminate) goto exit;
   int _rd = frame.ExecutionContext.ResumeDepth;
   frame.ExecutionContext.ResumeDepth = 0;
   if (_rd > 0) {
      int _retLabel = 0;
      int _popped = 0;
      while (_popped < _rd && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
      int _remaining = _rd - _popped;
      if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
      frame.ExecutionContext.ResumeDepth = _remaining;
      goto exit;
   }
}

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* (2035)	DO FORGET #1*/
#line hidden

label_2035: Console.Error.Write("2035 ");
Trace.WriteLine("[0072] ForgetStatement");
#line 76 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Forgetting 1");
{ int _n = (int)(1); for (int _i = 0; _i < _n && _nextStack.Count > 0; _i++) _nextStack.Pop(); }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .5 <- "?'.4~#1'$#2"~#3*/
#line hidden
Trace.WriteLine("[0073] CalculateStatement");
#line 77 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .5 <- {0}",(ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(frame.ExecutionContext[".4"]),(uint)(1))), 2)))),(uint)(3))));
frame.ExecutionContext[".5"] = (ulong)Lib.Select((uint)((Lib.Xor(Lib.Mingle(((ulong)Lib.Select((uint)(frame.ExecutionContext[".4"]),(uint)(1))), 2)))),(uint)(3));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO (2031) NEXT*/
#line hidden
Trace.WriteLine("[0074] NextStatement");
#line 78 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(18);
goto label_2031;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_18: ;
if (frame.ExecutionContext.Done) goto exit;

/* DO .2 <- .2~#65534*/
#line hidden
Trace.WriteLine("[0075] CalculateStatement");
#line 79 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .2 <- {0}",(ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(65534))));
frame.ExecutionContext[".2"] = (ulong)Lib.Select((uint)(frame.ExecutionContext[".2"]),(uint)(65534));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* DO .4 <- .4~#65534*/
#line hidden
Trace.WriteLine("[0076] CalculateStatement");
#line 80 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .4 <- {0}",(ulong)Lib.Select((uint)(frame.ExecutionContext[".4"]),(uint)(65534))));
frame.ExecutionContext[".4"] = (ulong)Lib.Select((uint)(frame.ExecutionContext[".4"]),(uint)(65534));

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE DO (2036) NEXT*/
#line hidden
Trace.WriteLine("[0077] NextStatement");
#line 81 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
#line hidden
_nextStack.Push(19);
goto label_2036;

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
_ret_19: ;
if (frame.ExecutionContext.Done) goto exit;

/* (2031)	DO (1001) NEXT*/
#line hidden

label_2031: Console.Error.Write("2031 ");
Trace.WriteLine("[0078] NextStatement");
#line 82 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Doing (1001) Next");;
{
   bool shouldTerminate = fullsyslibProp.DO_1001(frame.ExecutionContext);
   if (shouldTerminate) goto exit;
   int _rd = frame.ExecutionContext.ResumeDepth;
   frame.ExecutionContext.ResumeDepth = 0;
   if (_rd > 0) {
      int _retLabel = 0;
      int _popped = 0;
      while (_popped < _rd && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
      int _remaining = _rd - _popped;
      if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
      frame.ExecutionContext.ResumeDepth = _remaining;
      goto exit;
   }
}

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE DO .4 <- .1*/
#line hidden
Trace.WriteLine("[0079] CalculateStatement");
#line 83 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine(string.Format("       .4 <- {0}",frame.ExecutionContext[".1"]));
frame.ExecutionContext[".4"] = frame.ExecutionContext[".1"];

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE RETRIEVE .1 + .5*/
#line hidden
Trace.WriteLine("[0080] RetrieveStatement");
#line 84 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
Trace.WriteLine("       Retrieving .1");;
frame.ExecutionContext.Retrieve(".1");
Trace.WriteLine("       Retrieving .5");;
frame.ExecutionContext.Retrieve(".5");

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden

/* PLEASE RESUME #2*/
#line hidden
Trace.WriteLine("[0081] ResumeStatement");
#line 85 "C:\\projects\\intercal\\INTERCAL\\samples\\primes.i"
;
#line hidden
   {
      int depth = (int)(2);
      if (depth > 0) {
         int _retLabel = 0;
         int _popped = 0;
         while (_popped < depth && _nextStack.Count > 0) { _retLabel = _nextStack.Pop(); _popped++; }
         int _remaining = depth - _popped;
         if (_retLabel > 0 && _remaining == 0) { switch(_retLabel) { case 0: goto exit; case 1: goto _ret_1; case 2: goto _ret_2; case 3: goto _ret_3; case 4: goto _ret_4; case 5: goto _ret_5; case 6: goto _ret_6; case 7: goto _ret_7; case 8: goto _ret_8; case 9: goto _ret_9; case 10: goto _ret_10; case 11: goto _ret_11; case 12: goto _ret_12; case 13: goto _ret_13; case 14: goto _ret_14; case 15: goto _ret_15; case 16: goto _ret_16; case 17: goto _ret_17; case 18: goto _ret_18; case 19: goto _ret_19;  } }
         frame.ExecutionContext.ResumeDepth = _remaining;
         goto exit;
      }
   }

#line hidden
dot_1 = frame.ExecutionContext.GetVarValue(".1") ?? 0;
dot_10 = frame.ExecutionContext.GetVarValue(".10") ?? 0;
dot_11 = frame.ExecutionContext.GetVarValue(".11") ?? 0;
dot_12 = frame.ExecutionContext.GetVarValue(".12") ?? 0;
dot_13 = frame.ExecutionContext.GetVarValue(".13") ?? 0;
dot_2 = frame.ExecutionContext.GetVarValue(".2") ?? 0;
dot_3 = frame.ExecutionContext.GetVarValue(".3") ?? 0;
dot_4 = frame.ExecutionContext.GetVarValue(".4") ?? 0;
dot_5 = frame.ExecutionContext.GetVarValue(".5") ?? 0;

#line hidden
      //Generic catch-all if the program
      throw new Exception(Messages.E633);

   exit:
      return;
   }


fullsyslib m_fullsyslibProp;
fullsyslib fullsyslibProp
{
   get {if(m_fullsyslibProp== null) m_fullsyslibProp = new fullsyslib(); return m_fullsyslibProp;}
}
}

class entry
{
   static void Main(string[] args)
{
      //Speed up startup time by ensuring adequate thread availability
      System.Threading.ThreadPool.SetMinThreads(80, 4);

      try
      {
         var program = new primes();
         program.Run();
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
      }
   }
}
