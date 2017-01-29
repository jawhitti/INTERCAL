*The following was written by Jason Whittington, who
fervently denies authorship and requests complete anonymity.*

# INTERCAL
Official home of Simple Component Intercal for .NET

# Overview
Starting in the late 1990s Microsoft got bored with their
existing programming infrastructure and decided to scrap it in favor of
something slower.  Thus was born the effort to build a spiffy new programming
platform known as the Conveniently Limited Runtime, or CLR.  This platform had the
ludicrous ambition to provide services that would be consumable by a large
range of languages, allowing components written in a variety of different source
languages to coexist and interact. Designers of the platform got carried away
and made grandiose claims like: 

*The Common Type System (CTS) provides a rich type
system that supports the types and operations found in many programming
languages. The Common Type System is intended to support the complete
implementation of a wide range of programming languages."*

 After years of research and millions of marketing dollars the
platform was produced along with compilers for languages that ranged from
simple and easy (Figure 1) to complex and difficult (Figure 2).

``` 
Class Hello
   Shared Sub Main  
   System.Console.WriteLine("Hello, World")
   End Sub
End Class
```
Figure 1: A simple and easy language
```
class Hello
{
   static void Main()
   {
      System.Console.WriteLine("Hello, World");
   }
}
```

Figure 2: A complex and difficult language

Despite the impressive range already demonstrated by the two
languages above the question remained as to just how wide a range of languages
this platform could actually support. To really test the range would require a
really different language, one with
as little in common as possible with the languages already ported to the
platform.

 

A ready candidate was found in a language that dates all the
way back to 1972: INTERCAL the Computer Language with No Pronounceable Acronyms
(not coincidentally the author dates back to 1972 as well). INTERCAL was defined
by ambition completely antithetical to that of the CLR: to have a nothing at
all in common with any other major language. 

 

"Hello, World" in INTERCAL bears nothing in common
with either of the above languages, as can be seen in Figure 3:

 

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

 

Figure 3: Hello, World in INTERCAL 

 

Now that the right language had been identified the burning
questions needed to be answered: could INTERCAL be made to run on the CLR? Could
it really integrate with other languages? 
Wasn’t there anything better to do?

 

Disappointingly, Microsoft declined to fund a proposal for a
Maui-based research effort. Phone calls and emails went consistently unanswered.
Undaunted by Microsoft’s apparent indifference (or the resulting restraining
orders), the decision was hastily made to build an implementation. Thus was
born Simple INTERCAL for .NET, or SICK.NET.




#Other resources
[MuppetLabs Intercal Pages](http://www.muppetlabs.com/~breadbox/intercal/)
