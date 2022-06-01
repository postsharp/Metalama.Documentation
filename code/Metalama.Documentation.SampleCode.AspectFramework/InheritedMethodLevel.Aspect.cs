using Metalama.Framework.Aspects;
using System;

namespace Doc.InheritedMethodLevel
{
    [Inherited]
    internal class InheritedAspectAttribute : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {
            Console.WriteLine( "Hacked!" );
            return meta.Proceed();
        }
    }
}
