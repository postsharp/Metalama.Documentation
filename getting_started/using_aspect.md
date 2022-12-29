# Getting started using aspects
If you are programming a few years, you obviously noticed several ways your 
programs grow and reach out of bounds. So it becomes increasingly difficult to 
take care of them and scale them properly. Since there had been no support in the .NET languages to templatize recurring design patterns, it becomes impossible to get rid of so-called boilerplate codes programmatically. 
Granted, you can create reusable code in functions and types but then eventually your 
code will be spinkled with such code calls. 

Also, in green-field projects as an architect or team lead you start with a vision for code quality and architure best practices that you want to follow. However over time other commitments and duties like shipping before deadline and within budget becomes the priority. This results in huge technical-debt accumulated over time. I bet you wished for some magical thing that could help you stay on track for your architectural vision and prevent the so called architural-erosion from happening; so that you can not only ship good code on time but stay ready for obvious future enhancements that are already on your way! 

Aspect oritented programming can help you in these situatins and many more. This guide is written to aquanit you to the process of using aspects created using "Metalama" the world leading aspect oriented programming framework for C#

### What’s an Aspect? 
An aspect is a programming block that changes the behaviour of a system by attributing different capabilities that the original program lacked _at runtime_. The part of the original program on top of which the aspect is applied is called `Target`

A target can be *anything* inside a code block
For example, it can be 

* A class 
* A method 
* A parameter 


### Why I need one? 
An aspect can help you in multitude of situations. However all of these can be broadly classified under three different broad categories. 

* _Boilerplate Reduction_ 
     * No need to write the same thing over and over again to be used in different locations in the code. 
* _Code Generation_ 
     * Generate code that is needed but not necessarily business code automatically
	 everytime you need it.
* _Preventing Architectural Decay_
    * Prevent bad quality code check-ins by enforcing your coding standard via aspects 
	and other related tools like fabrics. 

### Meet your first aspect 

#### The situation 
Let's imagine a situation. You have many different method calls in a particular section of the code and you are not getting it to work. Somewhere the call chain breaks. The only way to track this is to put tracing logs at the entry and exit of each method calls and then try to read through the generated log to see if it works or not. But to achieve this you shall have to write trace-logging code at each and every method and function body. That's hard to the say the least. It is downright prohibitive if the number of involved methods are more than just a handful. 

Imagine you have the following method to update a database. 

```csharp
public bool UpdateDatabase(ItemDetails details)
{
   //checking if it is ok to update 
   bool ok = dbInstance.CheckConditions();
   //
   if (ok)
   {
	   UpdateCore(details);
   }
   //Updating the status that the update was successfull 
   UpdateStatus(details.Id);
}

```

#### The solution 
Now imagine that this method is broken and you need to do a continuity check. I bet you thought to yourself that if you had a magical way to log every step of the process at least on the function call level then it would have been much easier to debug. 


The following shows probably the simplest possible aspect you shall ever see. 

```csharp

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaLamaEx
{
    public class LogAttribute : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {
            string methodName = meta.Target.Method.ToDisplayString();

            Console.WriteLine($"Entering {methodName}");
            try
            {
                var result = meta.Proceed();
                Console.WriteLine($"Exiting {methodName}");
                return result;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
    }
}

```
Now you can use this newly created _aspect_ as an attribute like this 


```csharp
[Log]
public bool UpdateDatabase(ItemDetails details)
{
   //checking if it is ok to update 
   bool ok = dbInstance.CheckConditions();
   //
   if (ok)
   {
	   UpdateCore(details);
   }
   //Updating the status that the update was successfull 
   UpdateStatus(details.Id);
}

[Log]
public void UpdateCore(ItemDetails details)
{ 
}

[Log]
public void UpdateStatus(ItemDetails details)
{ 

}


```
### What is compile time and what’s runtime?
###	What can I do with Aspect 

### Can I debug an aspect that I am using ?
•	I have two aspects that I want to apply on my method in a particular order. How do I ensure that?
•	How can I make an aspect apply automatically to all code blocks?
•	How can I make an aspect omnipresent ?
•	Are there any ready-to-use aspects available?
•	Can I turn aspects on and off from a global configuration/compilation switch much like enabling and disabling breakpoints in a codebase?
