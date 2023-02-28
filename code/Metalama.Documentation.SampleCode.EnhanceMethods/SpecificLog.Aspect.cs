using Metalama.Framework.Aspects;
using System;
namespace Doc.SpecificLog
{
    public class SpecificLogAttribute : OverrideMethodAspect
    {

        public override dynamic OverrideMethod()
        {
            string targetMethodName = meta.Target.Method.ToDisplayString();
            try
            {
                Console.WriteLine($"Started executing {targetMethodName}");
                return meta.Proceed();
            }
            finally
            {
                Console.WriteLine($"Finished executing {targetMethodName}");
            }
        }
    }
}
