using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.CompileTimeIf
{
    internal class TargetCode
    {
        [CompileTimeIf]
        public void InstanceMethod()
        {
            Console.WriteLine($"Invoking TargetCode.InstanceMethod() on instance {base.ToString()}.");
            Console.WriteLine("InstanceMethod");
            return;
        }

        [CompileTimeIf]
        public static void StaticMethod()
        {
            Console.WriteLine($"Invoking TargetCode.StaticMethod()");
            Console.WriteLine("StaticMethod");
            return;
        }
    }
}
