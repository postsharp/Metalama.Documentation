using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.Ordering
{
    [Aspect1]
    [Aspect2]
    internal class TargetCode
    {
        public static void SourceMethod()
        {
            Console.WriteLine("Executing Aspect1 on SourceMethod. Methods present before applying Aspect1: SourceMethod, IntroducedMethod2");
            Console.WriteLine("Executing Aspect2 on SourceMethod. Methods present before applying Aspect2: SourceMethod");
            Console.WriteLine("Method defined in source code.");
            goto __aspect_return_1;
        __aspect_return_1:
            return;
        }


        public static void IntroducedMethod1()
        {
            Console.WriteLine("Method introduced by Aspect1.");
        }

        public static void IntroducedMethod2()
        {
            Console.WriteLine("Executing Aspect1 on IntroducedMethod2. Methods present before applying Aspect1: SourceMethod, IntroducedMethod2");
            Console.WriteLine("Method introduced by Aspect2.");
            return;
        }
    }

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Executing SourceMethod:");
            TargetCode.SourceMethod();

            Console.WriteLine("---");
            Console.WriteLine("Executing IntroducedMethod1:");
            TargetCode.IntroducedMethod1();

            Console.WriteLine("---");
            Console.WriteLine("Executing IntroducedMethod2:");
            TargetCode.IntroducedMethod2();
        }
    }
}