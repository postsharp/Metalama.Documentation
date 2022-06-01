using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.InheritedTypeLevel
{
    [Inherited]
    internal class InheritedAspectAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            foreach ( var method in builder.Target.Methods )
            {
                builder.Advice.Override( method, nameof( MethodTemplate ) );
            }

        }

        [Template]
        private dynamic? MethodTemplate()
        {
            Console.WriteLine( "Hacked!" );
            return meta.Proceed();
        }
    }
}
