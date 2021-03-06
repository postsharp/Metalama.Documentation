// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.CodeFixes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Doc.ToStringWithComplexCodeFix
{
    [AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
    [RunTimeOrCompileTime]
    public class NotToStringAttribute : Attribute { }

    [LiveTemplate]
    public class ToStringAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            base.BuildAspect( builder );

            // Suggest to switch to manual implementation.
            if ( builder.AspectInstance.Predecessors[0].Instance is IAttribute attribute )
            {
                builder.Diagnostics.Suggest(
                    new CodeFix( "Switch to manual implementation", codeFixBuilder => this.ImplementManually( codeFixBuilder, builder.Target ) ),
                    attribute );
            }
        }

        [CompileTime]
        private async Task ImplementManually( ICodeActionBuilder builder, INamedType targetType )
        {
            await builder.ApplyAspectAsync( targetType, this );
            await builder.RemoveAttributesAsync( targetType, typeof(ToStringAttribute) );
            await builder.RemoveAttributesAsync( targetType, typeof(NotToStringAttribute) );
        }

        [Introduce( WhenExists = OverrideStrategy.Override, Name = "ToString" )]
        public string IntroducedToString()
        {
            var stringBuilder = new InterpolatedStringBuilder();
            stringBuilder.AddText( "{ " );
            stringBuilder.AddText( meta.Target.Type.Name );
            stringBuilder.AddText( " " );

            var fields = meta.Target.Type.FieldsAndProperties.Where( f => !f.IsStatic ).ToList();

            var i = meta.CompileTime( 0 );

            foreach ( var field in fields )
            {
                if ( field.Attributes.Any( a => a.Type.Is( typeof(NotToStringAttribute) ) ) )
                {
                    continue;
                }

                if ( i > 0 )
                {
                    stringBuilder.AddText( ", " );
                }

                stringBuilder.AddText( field.Name );
                stringBuilder.AddText( "=" );
                stringBuilder.AddExpression( field.Invokers.Final.GetValue( meta.This ) );

                i++;
            }

            stringBuilder.AddText( " }" );

            return stringBuilder.ToValue();
        }
    }
}