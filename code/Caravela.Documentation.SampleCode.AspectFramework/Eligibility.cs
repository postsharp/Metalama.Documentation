using Caravela.Framework.Aspects;
using Caravela.Framework.Code;
using Caravela.Framework.Eligibility;
using System;
using System.IO;

namespace Caravela.Documentation.SampleCode.AspectFramework.Eligibility
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
