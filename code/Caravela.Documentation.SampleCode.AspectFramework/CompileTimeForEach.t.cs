using System;

namespace Caravela.Documentation.SampleCode.AspectFramework.CompileTimeForEach
{
    internal class TargetCode
    {
        [CompileTimeForEach]
        private void Method(int a, string b)
        {
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");
            Console.WriteLine("Hello, world.");
            return;
        }
    }
}
