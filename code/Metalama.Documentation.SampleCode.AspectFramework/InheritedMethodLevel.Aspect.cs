
using Metalama.Framework.Aspects;
using System;

namespace Doc.InheritedMethodLevel
{
    [Inheritable]
    internal class InheritedAspectAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( "Hacked!" );

            return meta.Proceed();
        }
    }
}