// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

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
                builder.Advice.Override( method, nameof(this.MethodTemplate) );
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