using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.UpdateMethod
{
    internal class UpdateMethodAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            var updateMethodBuilder = builder.Advice.IntroduceMethod( builder.Target, nameof(this.Update), buildMethod:
                m =>
                {
                    var fieldsAndProperties =
                     builder.Target.FieldsAndProperties
                   .Where( f => f.Writeability == Writeability.All );

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

                field.Invokers.Final.SetValue( meta.This, meta.Target.Parameters[index].Value );
                index++;
            }
        }
    }
}