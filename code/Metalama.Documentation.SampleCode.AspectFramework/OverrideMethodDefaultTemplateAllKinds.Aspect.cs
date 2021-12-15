using System;
using Metalama.Framework.Aspects;

namespace Metalama.Documentation.SampleCode.AspectFramework.OverrideMethodDefaultTemplateAllKinds
{
    public class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine($"{meta.Target.Method.Name}: start");
            var result = meta.Proceed();
            Console.WriteLine($"{meta.Target.Method.Name}: returning {result}.");
            return result;
        }
    }
}
