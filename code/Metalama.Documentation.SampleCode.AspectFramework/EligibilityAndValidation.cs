using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;
using System.IO;

namespace Metalama.Documentation.SampleCode.AspectFramework.EligibilityAndValidation
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
