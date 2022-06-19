// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.CodeFixes;
using System;
using System.Linq;

namespace Doc.ToStringWithSimpleToString
{
    [AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
    public class NotToStringAttribute : Attribute { }

    [LiveTemplate]
    public class ToStringAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            base.BuildAspect( builder );

            // For each field, suggest a code fix to remove from ToString.
            foreach ( var field in builder.Target.FieldsAndProperties.Where( f => !f.IsStatic ) )
            {
                if ( !field.Attributes.Any( a => a.Type.Is( typeof(NotToStringAttribute) ) ) )
                {
                    builder.Diagnostics.Suggest( CodeFixFactory.AddAttribute( field, typeof(NotToStringAttribute), "Exclude from [ToString]" ), field );
                }
            }
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