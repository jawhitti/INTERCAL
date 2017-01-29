
# SICK - Simple Component Intercal for .NET
Official home of Simple Component Intercal for .NET (SICK)

# Introduction
The following was written by Jason Whittington, who
fervently denies authorship and requests complete anonymity.

# Origin and Purpose
Starting in the late 1990s Microsoft got bored with their
existing programming infrastructure and decided to scrap it in favor of
something slower.  Thus was born the effort to build a spiffy new programming
platform known as the "Comically Limited Runtime ("CLR")".  
  
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
this platform could actually support. A ready candidate can be found in a language
that dates all the way back to 1972: INTERCAL the Computer Language with No Pronounceable Acronyms
(not coincidentally the author dates back to 1972 as well). INTERCAL was defined
by ambition completely antithetical to that of the CLR: *to have a nothing at
all in common with any other major language.*
  
"Hello, World" in INTERCAL indeed bears nothing in common with either of the above languages, 
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


## Component-Oriented INTERCAL
Almost all existing INTERCAL compilers are either interpreters or can only produce standalone
executables. One of the major goals of this project was to make INTERCAL available in the world of 
*components*.  Specifically:
  
* It is possible to compile standalone library and executable assemblies.  
* INTERCAL libraries can be consumed from other INTERCAL programs and libraries.  
* Variable-sharing and flow-control are supported between components with some minor restrictions.
* It is possible to author assemblies in other languages and consume them from INTERCAL.
* It is possible (but not really advisable) to consume INTERCAL libraries from other languages.


## Acronyms
This project is officially Standard Component Intercal for .NET ("SICK.NET"). Inspired by,
and building on Eric Raymond's work on **ick**, the compiler for this project is 
known as **sick**.

# Project contents
This project contains a Visual Studio 2015 solution with two main project:
* **sick.exe** - A conforming INTERCAL compiler for .NET 
* **intercal.runtime.dll** - A .NET assembly providing the standard system library and support code for the execution engine.
  
You should be able to download the code, load it up and build with ordinary "Ctrl-Shift-B".  A selection of sample 
INTERCAL programs is available in the "Samples" folder.

# Dependencies
This compiler is written in C# and targets **.NET 4.0**.  **sick.exe** is modeled equally after Eric Raymond's 
classic **ick** and the C# **csc** compilers.
   
# SICK user manual

### Compiling standalone applications
Standalone .exe applications are produced via the command-line switch "/t:exe", *i.e.* given this code in **app.i**.

```
DO .1 <- #32767
PLEASE READ OUT .1
DO GIVE UP
```
you could compile it using the following command line:

```
sick app.i
```
This will produce an executable (.exe).
  

### Compiling Libraries
Larger programs can use library assemblies to control size and complexity of source files and 
interop with other .NET languages. Libraries are produced via the command-line switch "/t:library".  
Any INTERCAL source file can be compiled into an library - there is nothing in the language
precluding it. By default all labels in the library are exposed publicly but this can be overridden.


As an example, you could extract a library from app.i above into following brief bit of source code:	

```
(100) DO .1 <- #32767
PLEASE RESUME #1
```
If you store this line of code into lib.i you can compile it into a Library Assembly via the following
```
sick /t:library lib.i
```  

This will produce a .NET Assembly lib.dll. This DLL will expose a public class with a 
public static method DO_100().
 
### Referencing Libraries
Libraries are referenced via the "/r:*<library_name.dll>*" command-line parameter.

All labels exposed as public by mylib.dll are available via DO...NEXT.  So for example given the library
lib.dll created above you could rewrite app.i as:  

```
DO (100) NEXT
PLEASE READ OUT .1
DO GIVE UP
```
This can them be compiled by referencing it via "/r" as shown below
```
sick /r:bar.dll app.i
```

and the execute it:
```
app
32767
```
* *The sharp-eyed reader has probably noticed the "wimpmode" output -- see "Limitations" below 

**NOTES**
* Library developers must ensure that all publicly exposed code paths eventually terminate
in a RESUME or GIVE UP.
* Multiple libraries can be specified via a comma-delimeted list
* If any referenced label cannot be found in the list of  referenced assembly compilation will fail with the message
**"E129 PROGRAM HAS GOTTEN LOST ON THE WAY TO <label>"**".

* If any referenced assembly cannot be found at compile-time compilation will fail with 
message **"E2002 SOME ASSEMBLY REQUIRED"**.


### Cross-language Support
Since SICK-compiled applivations are able to call compiled DLLs you can author extension DLLs in lesser
languages and invoke them via DO...NEXT.  The "csharplib" sample shows the way this is accomplished.  
(See the sample for more info).  The code snipped below gives a quick look at the idea:

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

### Debugging Support
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


### Programming Restrictions
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
* Only wimpmode numbering is currently supporte


### The standard library (syslib.i)
The standard distribution holds a compiled version of **syslib.i** in *intercal.runtime.dll*. 
This assembly is reference by default for all programs compiled with sick.exe.

# Acknowledgements
* This project drew inspiration primarily from Eric Raymond's [C-Intercal implementation (ick)](http://www.muppetlabs.com/~breadbox/intercal-man/s10.html).  
* Development would not have been possible without the [MuppetLabs Intercal Pages](http://www.muppetlabs.com/~breadbox/intercal/)
