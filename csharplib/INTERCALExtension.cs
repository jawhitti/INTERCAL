using System;
using System.Windows.Forms;
using INTERCAL.Runtime;

//Use this syntax to route jumps to a particular label to a method on
//a class.  The method may be static or public. and it can take
//either one or two arguments. The "Label" argument
//is optional - leaving it off will route *all* labels to the passed 
//fuction. Applying more than one attribute with the Label set to null
//will result in undefined behavior (as will multiple attributes with
//the same label pointing to different functions).
[assembly: EntryPoint("(3000)", "CSIntercalLib", "foobar")]


public class CSIntercalLib
{
    
	public bool foobar(ExecutionContext ctx)
	{
		MessageBox.Show("Hello From Intercal!");
		ctx[".3"] = ctx[".2"] + ctx[".1"];

        //This is an artifact of the way the nexting stack works.
        //Spinning up new threads is currently done here in  
        //extension libraries typically by calling ctx.Eval(proc, label).
        //See, e.g. sicklib.cs for an example.

        //Since this function is being called inline a DO...NEXT to an
        //extension proc does not automatically spin up a new thread.

        //The good news is that extension procs like this don't have 
        //to worry about calling RESUME - just return false for ordinary
        //methods. You can return true if you want to cause 
        return false;
	}
}