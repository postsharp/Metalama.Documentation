using Metalama.Framework.Aspects;
using System;
using System.Diagnostics;


namespace Doc.TimeIt
{
    public class TimeItAttribute : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {

            string name = meta.Target.Method.ToDisplayString();
            Console.WriteLine($"Started executing {name}");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = meta.Proceed();
            sw.Stop();
            Console.WriteLine($"Finished executing {name}");
            Console.WriteLine($"Time taken :{sw.ElapsedMilliseconds} ms");
            return result;
        }   
    }
}
