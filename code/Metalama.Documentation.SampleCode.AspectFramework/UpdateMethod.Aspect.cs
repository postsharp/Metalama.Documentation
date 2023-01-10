// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System.Linq;

namespace Doc.UpdateMethod
{
    internal class UpdateMethodAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            builder.Advice.IntroduceMethod(
                builder.Target,
                nameof(this.Update),
                buildMethod:
                m =>
                {
                    var fieldsAndProperties =
                        builder.Target.FieldsAndProperties
                            .Where( f => !f.IsImplicitlyDeclared && f.Writeability == Writeability.All );

                    foreach ( var field in fieldsAndProperties )
                    {
                        m.AddParameter( field.Name, field.Type );
                    }
                } );
        }

        [Template]
        public void Update()
        {
            var index = meta.CompileTime( 0 );

            foreach ( var parameter in meta.Target.Parameters )
            {
                var field = meta.Target.Type.FieldsAndProperties.OfName( parameter.Name ).Single();

                field.ToExpression().Value = meta.Target.Parameters[index].Value;
                index++;
            }
        }
    }
}