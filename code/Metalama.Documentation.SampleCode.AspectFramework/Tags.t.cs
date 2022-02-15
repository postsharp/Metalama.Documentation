using System;

namespace Doc.Tags
{
    internal class TargetCode
    {
        [TagsAspect]
        private void Method(int a, int b)
        {
            Console.WriteLine($"This method has 2 parameters.");
            Console.WriteLine($"Method({a}, {b})");
            return;
        }
    }
}