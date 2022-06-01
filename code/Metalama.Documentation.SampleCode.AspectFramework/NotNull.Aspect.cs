using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.NotNull
{
    internal class NotNullAttribute : ContractAspect
    {
        public override void Validate( dynamic? value )
        {
            if ( value == null )
            {
                var parameterName = meta.Target.Declaration switch
                {
                    IParameter parameter => parameter.Name,
                    IFieldOrProperty field => field.Name,

                    // Should not happen.
                    _ => meta.Target.ToString(),
                };

                if ( meta.Target.ContractDirection == ContractDirection.Input )
                {
                    throw new ArgumentNullException( parameterName );
                }
                else
                {
                    throw new PostConditionFailedException( $"'{parameterName}' cannot be null when the method returns." );

                }
            }
        }
    }
}
