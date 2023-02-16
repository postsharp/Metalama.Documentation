// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;
using System.Linq;

namespace Doc.ToString
{
    internal class ToStringAttribute : TypeAspect
    {
        [Introduce( WhenExists = OverrideStrategy.Override, Name = "ToString" )]
        public string IntroducedToString()
        {
            var stringBuilder = new InterpolatedStringBuilder();
            stringBuilder.AddText( "{ " );
            stringBuilder.AddText( meta.Target.Type.Name );
            stringBuilder.AddText( " " );

            var fields = meta.Target.Type.FieldsAndProperties.Where( f => !f.IsImplicitlyDeclared && !f.IsStatic ).ToList();

            var i = meta.CompileTime( 0 );

            foreach ( var field in fields )
            {
                if ( i > 0 )
                {
                    stringBuilder.AddText( ", " );
                }

                stringBuilder.AddText( field.Name );
                stringBuilder.AddText( "=" );
                stringBuilder.AddExpression( field.Value );

                i++;
            }

            stringBuilder.AddText( " }" );

            return stringBuilder.ToValue();
        }
    }
}