// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.NotNull
{
    internal class NormalizeIdAttribute : ContractAspect
    {
        public override void Validate( dynamic? value )
        {
            value = value?.Trim()?.ToUpper();
        }
    }
}