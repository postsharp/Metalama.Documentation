using System;

namespace Doc.CompileTimeForEach
{
    internal class TargetCode
    {
        [CompileTimeForEach]
        private void Method(int a, string b)
        {
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");
            Console.WriteLine($"Hello, world! a={a}, b='{b}'.");
            return;
        }
    }
}