
# INTERCAL
Official home of Simple Component Intercal for .NET

# Introduction
The following was written by Jason Whittington, who
fervently denies authorship and requests complete anonymity.

# Origin and Purpose
Starting in the late 1990s Microsoft got bored with their
existing programming infrastructure and decided to scrap it in favor of
something slower.  Thus was born the effort to build a spiffy new programming
platform known as the "Comically Limited Runtime ("CLR")".  This platform had the
ludicrous ambition to provide services that would be consumable by a large
range of languages, allowing components written in a variety of different source
languages to coexist and interact. Designers of the platform got carried away
and made grandiose claims like: 

*The Common Type System (CTS) provides a rich type
system that supports the types and operations found in many programming
languages. The Common Type System is intended to support the complete
implementation of a wide range of programming languages."*

Today, the .NET platform supports a range of languages from the 
simple and easy (Figure 1) to the complex and difficult (Figure 2).

``` 
Class Hello
   Shared Sub Main  
   System.Console.WriteLine("Hello, World")
   End Sub
End Class
```
**Figure 1**: A simple and easy language
```
class Hello
{
   static void Main()
   {
      System.Console.WriteLine("Hello, World");
   }
}
```

**Figure 2**: A complex and difficult language

Despite the impressive range already demonstrated by the two
languages above the question remained as to just how wide a range of languages
this platform could actually support. To really test the range would require a
really different language, one with as little in common as possible with the 
languages already ported to the platform.

A ready candidate was found in a language that dates all the
way back to 1972: INTERCAL the Computer Language with No Pronounceable Acronyms
(not coincidentally the author dates back to 1972 as well). INTERCAL was defined
by ambition completely antithetical to that of the CLR: *to have a nothing at
all in common with any other major language.*
  
"Hello, World" in INTERCAL bears nothing in common with either of the above languages, 
as can be seen in Figure 3:

 
```
DO ,1 <- #13
PLEASE DO ,1SUB#1 <- #234
DO ,1SUB#2 <- #112
DO ,1SUB#3 <- #112
DO ,1SUB#4 <- #0
DO ,1SUB#5 <- #64
DO ,1SUB#6 <- #194
DO ,1SUB#7 <- #48
PLEASE DO ,1SUB#8 <- #22
DO ,1SUB#9 <- #248
DO ,1SUB#10 <- #168
DO ,1SUB#11 <- #24
DO ,1SUB#12 <- #16
DO ,1SUB#13 <- #214
PLEASE READ OUT ,1
PLEASE GIVE UP
```  
**Figure 3**: Hello, World in INTERCAL 


Now that the right language had been identified the burning
questions needed to be answered: could INTERCAL be made to run on the CLR? Could
it really integrate with other languages? Wasn’t there anything better to do?

## Acronyms
This project is officially "Simple Component Intercal for .NET" ("SICK.NET"). Inspired by,
and building on Eric Raymond's work the compiler for this project is contained in *sick.exe*.

# The SICK Compiler

##
This compiler is written in C# and targets **.NET 4.0**.  *sick.exe* is modeled equally after Eric Raymond's 
classic *ick* and the C# *csc.exe* compilers.
 
## Debugging
*sick.exe* will add diagnostic Tracestatements to compiled executables if "/debug+" is specified on the command-line.
These statements can be captured at runtime setting up a standard .NET config file for compiled binaries, e.g:
```
<?xml version="1.0"?>
<configuration>
  <system.diagnostics>
    <trace autoflush="true" indentsize="4">
      <listeners>
   	<add name="textListener" 
             type="System.Diagnostics.TextWriterTraceListener" 
             initializeData="primes.log" />
        <remove name="Default" />
      </listeners>
    </trace>
  </system.diagnostics> 
</configuration>

```

# Innovation
This compiler follows C-INTERCAL in spirit but in keeping with the spirit of the INTERCAL community this 
project introduces a few unique new capabilities.
    
## Component-Oriented Intercal
Almost all existing INTERCAL compilers are either interpreters or can only produce standalone
executables.  SICK extends the INTERCAL execution model to include **components**.  Specifically:
  
* It is possible to compile standalone library and executable assemblies with **sick.exe**.  
* INTERCAL libraries can be consumed from INTERCAL programs (or from other libraries) via the standard DO...NEXT construct
* Variable-sharing and flow-control are supported between components with some minor restrictions.
* It is possible to author assemblies in other languages and consume them from INTERCAL.


### Compiling libraries
Libraries are produced via the command-line switch "/t:library".  Any INTERCAL source file can be 
compiled into a library.  By default all labels in the library are exposed public via DO_<label>
entry stubs.

As an example, consider the following brief bit of source code:	

```
(100) DO .1 <- #32767
PLEASE RESUME #1
```
If you store this line of code into bar.i you can compile it into a Library Assembly via the following
```
sick /t:library bar.i
```  

This will produce a .NET Assembly bar.dll (as well as a file *~tmp.cs* which is left on the disk on purpose).
This DLL will expose a public class with a public static method DO_100().
 

### Compiling standalone applications
Standalone .exe applications are produced via the command-line switch "/t:exe". 
```
sick code.i
```
  
### Referencing Libraries
Libraries are referenced via the "/r:*<library_name.dll>*" command-line parameter.

```
sick /r:mylib.dll drain.i
```

All labels exposed as public by mylib.dll are available via DO...NEXT.  So for example given (or sold) 
a file foo.i with the following contents:  

```
DO (100) NEXT
PLEASE READ OUT .1
DO GIVE UP
```

you could compile it via the following
```
sick /r:bar.dll foo.i
```

and the execute it:
```
foo
32767
```

### Cross-language Support
Since SICK-compiled applivations are able to call compiled DLLs you can author extension DLLs in lesser
languages and invoke them via DO...NEXT.  The "csharplib" sample shows the way this is accomplished.  
(See the sample for more info).

```
using System;
using System.Windows.Forms;
using INTERCAL.Runtime;

[assembly: EntryPoint("(3000)", "CSIntercalLib", "foobar")]
public class CSIntercalLib
{
    
	public bool foobar(ExecutionContext ctx)
	{
		MessageBox.Show("Hello From Intercal!");
		ctx[".3"] = ctx[".2"] + ctx[".1"];
	        return false;
	}
}
``` 

### Component Restrictions
Some INTERCAL constructs are only mapped locally to their hosting component in order
 to preserve component boundaries. These include:

#### NEXT / RESUME / FORGET
intercal.runtime.dll implements a thread-based NEXTing stack that allows full NEXT/RESUME/FORGET 
support, even between components.  The comment in twisty.cs explains:
```
    //The code in this file implements a thread-based NEXTing stack. This is challenging
    //because the sematics of FORGET mean that entries can be dropped from the call stack.
    //In an ordinary program a DO NEXT would invoke a subroutine that would always return 
    //control back to the parent.  FORGET allows the child to never return, so  DO NEXT / FORGET
    //pairs can be used to move control around willy-nilly, (subject to the 80-item max 
    //NEXTING depth).  When all code is in a single component a goto-based solution is adequate
    //but linking multiple components together is a bigger challenge.

    //#I deals with this by using a thread-based nexting stack.  When a DO NEXT is encountered 
    //the compiler generates a call to ExecutionContext.Evaluate, passing it a delegate referencing 
    //a function (typically "Eval()" as well as a label parameter.  This call fires the delegate
    //asynchronously on the .NET thread pool and waits for it to complete.  The fuction will 
    //eventually either RESUME (returning false) or will FORGET and return true (GIVE UP is basically a fancy FORGET).  
    //Either of these conditions will release the calling thread which will then continue (in the
    //case of RESUME) or immediately exit (in the case of FORGET).  
```
Please do note that libraries must still ensure that every code path evenutally ends in a RESUME or GIVE UP.

#### COME FROM
It is only legal to COME FROM a label local to the current component. 
It is not possible to COME FROM another component.

#### ABSTAIN / REINSTATE
ABSTAIN and REINSTATE calls only act on the local component (this includes gerunds)

#### IGNORE/REMEMBER
It is not legal to IGNORE or REMEMBER labels that exist outside of the calling component.

#### READ OUT/WRITE IN
sick bases its I/O on the "Turing Text Model" from c-intercal).  This presents difficulties for
component based systems because the tape, because the tape is a *shared device*.  The original 
Turing Text model would not work for component software, as it is impossible to decode a 
string stored in an intercal array unless you know what position the tape head was at the 
beginning (or end) of a READ OUT or WRITE IN operation.  At the moment this means 
string data cannot be easily exchanged between components.

To make it possible to exchange string data the SICK compiler makes the “current” read and write 
position available in variables .999 and .9999,  respectively. INTERCAL components that want to 
pass string data out to the outside world should first copy the contents of these variables 
into other variables so that client code can decode strings.  For example:
```
DO .1 <- .999
DO WRITE IN ;1  //implicitly modifies .999 
DO .2 <- .999
DO WRITE IN ;2  //implicitly modifies .999 again
DO (3000) NEXT //where 3000 is in another lib
```
This allows the implementer of (3000) to decode both ;1 and ;2.  If (3000) wants to write 
out variables then it can query .9999.



### Compiler and runtime Limitations
* Only wimpmode numbering is currently supported

#### Notes:
* If no referenced assembly exposes the named endpoint compilation will fail with 
"E129 PROGRAM HAS GOTTEN LOST ON THE WAY TO *(<label>)*".

* If any referenced assembly cannot be found at compile-time compilation will fail with message "E2002 SOME ASSEMBLY REQUIRED".


### The standard library (syslib.i)
The standard distribution holds a compiled version of **syslib.i** in *intercal.runtime.dll*. 
This assembly is reference by default for all programs compiled with sick.exe.

# Acknowledgements
* This project drew inspiration primarily from Eric Raymond's [C-Intercal implementation (ick)](http://www.muppetlabs.com/~breadbox/intercal-man/s10.html).  
* Development would not have been possible without the [MuppetLabs Intercal Pages](http://www.muppetlabs.com/~breadbox/intercal/)
