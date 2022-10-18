using System;

namespace Doc.CompileTimeIf
{
    internal class Foo
    {
        [CompileTimeIf]
        public void InstanceMethod()
        {
            Console.WriteLine($"Invoking Foo.InstanceMethod() on instance {base.ToString()}.");
            Console.WriteLine("InstanceMethod");
            return;
        }

        [CompileTimeIf]
        public static void StaticMethod()
        {
            Console.WriteLine("Invoking Foo.StaticMethod()");
            Console.WriteLine("StaticMethod");
            return;
        }
    }
}