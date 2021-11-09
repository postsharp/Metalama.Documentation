using Caravela.Framework.Aspects;
using Caravela.Framework.Code;
using Caravela.Framework.Eligibility;
using System;
using System.IO;

namespace Caravela.Documentation.SampleCode.AspectFramework.EligibilityAndValidation
{
    internal class SomeClass
    {
        object? logger;

        [Log]
        private void InstanceMethod() { }


        [Log]
        private static void StaticMethod() { }
        
    }

}
