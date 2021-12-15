using System;
using System.Linq;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Metalama.Documentation.SampleCode.AspectFramework.UpdateMethod
{
    class UpdateMethodAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            var updateMethodBuilder = builder.Advices.IntroduceMethod(builder.Target, nameof(Update));

            var fieldsAndProperties = 
                builder.Target.FieldsAndProperties
                .Where(f => f.Writeability == Writeability.All);

            foreach ( var field in fieldsAndProperties)
            {
                updateMethodBuilder.AddParameter(field.Name, field.Type);
            }
        }

        [Template]
        public void Update()
        {
            var index = meta.CompileTime(0);

            foreach ( var parameter in meta.Target.Parameters )
            {
                var field = meta.Target.Type.FieldsAndProperties.OfName(parameter.Name).Single();

                field.Invokers.Final.SetValue(meta.This, meta.Target.Parameters[index].Value);
                index++;
            }
        }
    }
}
