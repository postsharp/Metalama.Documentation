// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable CS8602  // Dereference of a possibly null reference.

namespace Doc.Trim
{
    internal class TrimAttribute : ContractAspect
    {
        public override void Validate( dynamic? value )
        {
            if ( ((IHasType) meta.Target.Declaration).Type.IsNullable == true )
            {
                value = value?.Trim();
            }
            else
            {
                value = value.Trim();
            }
        }
    }
}