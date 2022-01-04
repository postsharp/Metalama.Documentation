using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.PrintFieldValues
{
    internal class TargetCode
    {
        private readonly int _a;

        public string? B { get; set; }

        private static readonly int _c;

        [PrintFieldValues]
        public void Method()
        {
            var value = _a;
            Console.WriteLine($"_a={value}");
            var value_1 = _c;
            Console.WriteLine($"_c={value_1}");
            var value_2 = B;
            Console.WriteLine($"B={value_2}");
            return;
        }

    }
}
