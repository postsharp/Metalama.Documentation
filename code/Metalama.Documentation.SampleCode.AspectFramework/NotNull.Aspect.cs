// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

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
                if ( meta.Target.ContractDirection == ContractDirection.Input )
                {
                    throw new ArgumentNullException( nameof(value) );
                }
                else
                {
                    throw new PostConditionFailedException( $"'{nameof(value)}' cannot be null when the method returns." );
                }
            }
        }
    }
}