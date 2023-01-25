// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.NotNullFabric
{
    internal class NotNullAttribute : MethodAspect
    {
        public override void BuildAspect( IAspectBuilder<IMethod> builder )
        {
            base.BuildAspect( builder );

            foreach ( var parameter in builder.Target.Parameters.Where(
                         p => p.RefKind is RefKind.None or RefKind.In
                              && !p.Type.IsNullable.GetValueOrDefault()
                              && p.Type.IsReferenceType.GetValueOrDefault() ) )
            {
                builder.Advice.AddContract( parameter, nameof(this.Validate), args: new { parameterName = parameter.Name } );
            }
        }

        [Template]
        private void Validate( dynamic? value, [CompileTime] string parameterName )
        {
            if ( value == null )
            {
                throw new ArgumentNullException( parameterName );
            }
        }
    }

    internal class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Outbound.SelectMany(
                    a => a.Types
                        .Where( t => t.Accessibility == Accessibility.Public )
                        .SelectMany( t => t.Methods )
                        .Where( m => m.Accessibility == Accessibility.Public ) )
                .AddAspect<NotNullAttribute>();
        }
    }
}