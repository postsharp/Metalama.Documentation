using System;
using System.IO;

namespace Metalama.Documentation.SampleCode.AspectFramework.Eligibility
{
    internal class SomeClass
    {
        private TextWriter _logger = Console.Out;

        [Log]
        private void InstanceMethod() { }


        [Log]
        private static void StaticMethod() { }
        
    }

}
