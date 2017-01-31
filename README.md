
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
**intercal.runtime.dll** implements a thread-based NEXTing stack that allows full NEXT/RESUME/FORGET 
support, even between components. This was perhaps the biggest challenge to "componentizing"
INTERCAL and the implementation deserves some description.
  
The NEXTING stack is challenging because the sematics of FORGET mean that entries can be dropped from the *middle* of the call stack. In an ordinary language a construct like DO NEXT would invoke a subroutine that would always return control back to the parent. INTERCAL is of course different. FORGET allows the child to never return, so  DO NEXT / FORGET pairs can be used to move control around willy-nilly, (subject to the 80-item max NEXTING depth, of course).  When all code is in a single component a goto-based solution is adequate but linking multiple components together is a bigger challenge. If an application .exe calls into a .dll and the function in the dll FORGETs we have a real problem.  There's no way to implement a program that doesn't have the thread unwind back to where it started. 

The first attempt at implementing these semantics involved functions tracking how many times FORGET and RESUME were called and making adjustments on return.  This worked logically (and was able to successfully run most INTERCAL programs but primes.i and pi.i both broke because even though the generated code maintained a *logical* depth correctly it still leaked *physical* frames and eventually ran out of stack space and died.
  
After a period of gnashing of teeth and rending of garments a new strategy was implemented. This strategy is built on the observation that if you invoke a new **thread** at every DO NEXT the problem can be solved reasonably elegantly. This is particularly appealing on the .NET framework because the ThreadPool, Monitor-based signaling, and async delegates make it pretty straightforward to put together.
  
So here's how it works. When a DO NEXT is encountered the compiler generates a call to ExecutionContext.Evaluate, passing it a delegate referencing a function (typically "Eval())" as well as a label. This call blocks inside the runtime while the target is scheduled to run on the thread pool. The invoked function must eventually call back into the runtime to signal Resume() or Forget(). If the invoked function calls Resume() the waiting thread is popped and the waiting function is released to continue. The topmost function then exits (and the thread is released back to the threadpool).  If the topmost thread calls Forget() it continues running.  The other threads below it on the stack are signaled to terminate and they exit.  

In this manner the NEXT semantics can be preserved across components. An application (.exe) can call DO NEXT into a Library (.dll). At this point there are two threads on the NEXT stack - the bottommost originating from Main() and the topmost originating from the invoked function in the DLL.  If the DLL calls FORGET #1 the bottom-most function will exit and the stack will contain only the currently executing thread. There are some complications here involving foreground and background threads, but the basic idea remains.

Please do note that libraries must still ensure that every code path evenutally ends in a RESUME or GIVE UP.  
True to form executing a GIVE UP does indeed exit the entire program.  **sick.exe** does all of this automatically for INTERCAL programmers. Developers interested in building extensions in other languages can participate in the scheme but sadly most will not be interested and will implement interoperability using straight function calls.  Sad! 

##### External calls
It's interesting to note that the only reason this chicanery is needed is to support FORGET. This means that 
in the case of interop where an ordinary function call is being invoked the thread creation can be skipped in favor of a direct function call. 
  
##### Future research  
If one could construct a flow-graph of an INTERCAL input program it may be possible to prove that certain sequences will never result in a FORGET. Such sequences could use direct function linkage and allow significant optimization as NEXT/RESUME is far more popular than NEXT/FORGET.   It may also be possible to detect the case where DO NEXT is followed immediately by "FORGET #1" in which case the code generator could emit an ordinary goto (at least for local calls).
 

#### COME FROM
It is only legal to COME FROM a label local to the current component.  It is not possible to COME FROM another component.

#### ABSTAIN / REINSTATE
ABSTAIN and REINSTATE calls only act on the local component (this includes gerunds)

#### IGNORE/REMEMBER
It is not legal to IGNORE or REMEMBER labels that exist outside of the calling component.

#### READ OUT/WRITE IN
The runtime bases its I/O on the ["Turing Text Model"](http://www.muppetlabs.com/~breadbox/intercal-man/s05.html) 
first implemented in c-intecal.  This presents difficulties for
component based systems because the tape is a *shared device*.  The original 
Turing Text model would not work for component software, as it is impossible to decode a 
string stored in an intercal array unless you know what position the tape head was at the 
beginning (or end) of a READ OUT or WRITE IN operation. 

To make it possible to exchange string data the SICK runtime makes the “current” read and write 
position available in properties LastIn and LastOut. These variables are not directly accessible
from INTERCAL source code but they are publicly accessible. 


### Compiler and runtime Limitations
* The front-end parser is currently a really dodgy regex affair because in 2003 I couldn't make 
any parser-generators work in C#. As of late 2016 I was able to produce a functioning INTERCAL 
recognizer using ANTLR but I haven't grafted it onto the code generator yet.

* I am not entirely happy with the ExecutionContext set of classes. I want to refactor them to present a better set of services to client code.
  
* I/O is still strictly ordinary.  To input the value 1023 type "1023".

* I/O has a funky issue on a few programs like echo.i and rot13.i. If you compile up echo.exe, run it, 
and enter "abc123" the program will appear to malfunction and only print out output value:
```
.\echo.exe
abc123
a
```
However if you hit start typing, hit a control key or even click on a different window the rest of the
string will magically appear.


### The standard library (syslib.i)
The standard distribution holds a compiled version of **syslib.i** in *intercal.runtime.dll*. 
This assembly is reference by default for all programs compiled with sick.exe.

# OTher resources
* The [C-INTERCAL Git Repe](https://github.com/calvinmetcalf/intercal) contains a wealth of code. The [pit](https://github.com/calvinmetcalf/intercal/tree/master/pit) is probably the most complete collection of intercal code and docs anywhere. The "libs" folder in particular holds floating point and other useful bits. 

# Acknowledgements
* This project drew inspiration primarily from Eric Raymond's [C-Intercal implementation (ick)](http://www.muppetlabs.com/~breadbox/intercal-man/s10.html).  
* Development would not have been possible without the [MuppetLabs Intercal Pages](http://www.muppetlabs.com/~breadbox/intercal/)
