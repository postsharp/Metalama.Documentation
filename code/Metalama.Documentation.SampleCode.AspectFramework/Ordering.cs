using System;
using Metalama.Framework.Aspects;


namespace Metalama.Documentation.SampleCode.AspectFramework.Ordering
{
    [Aspect1, Aspect2]
    internal class TargetCode
    {
        public static void SourceMethod()
        {
            Console.WriteLine("Method defined in source code.");
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
#if TESTRUNNER
             TargetCode.IntroducedMethod1();
#endif

            Console.WriteLine("---");
            Console.WriteLine("Executing IntroducedMethod2:");
#if TESTRUNNER
             TargetCode.IntroducedMethod2();
#endif
        }

    }
}