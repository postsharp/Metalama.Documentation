using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;
using System.IO;

namespace Metalama.Documentation.SampleCode.AspectFramework.Eligibility
{
    internal class SomeClass
    {
        TextWriter logger = Console.Out;

        [Log]
        private void InstanceMethod() { }


        [Log]
        private static void StaticMethod() { }
        
    }

}
