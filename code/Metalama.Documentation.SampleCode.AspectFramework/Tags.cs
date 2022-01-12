using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.Tags
{
    internal class TargetCode
    {
        [TagsAspect]
        private void Method(int a, int b)
        {
            Console.WriteLine($"Method({a}, {b})");
        }
    }
}
