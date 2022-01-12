// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Metalama.Documentation.SampleCode.AspectFramework.UpdateMethod
{
    internal class UpdateMethodAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            var updateMethodBuilder = builder.Advices.IntroduceMethod( builder.Target, nameof(this.Update) );

            var fieldsAndProperties =
                builder.Target.FieldsAndProperties
                    .Where( f => f.Writeability == Writeability.All );

            foreach ( var field in fieldsAndProperties )
            {
                updateMethodBuilder.AddParameter( field.Name, field.Type );
            }
        }

        [Template]
        public void Update()
        {
            var index = meta.CompileTime( 0 );

            foreach ( var parameter in meta.Target.Parameters )
            {
                var field = meta.Target.Type.FieldsAndProperties.OfName( parameter.Name ).Single();

                field.Invokers.Final.SetValue( meta.This, meta.Target.Parameters[index].Value );
                index++;
            }
        }
    }
}