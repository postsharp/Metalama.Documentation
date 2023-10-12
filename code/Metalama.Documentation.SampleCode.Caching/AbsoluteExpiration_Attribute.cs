// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.AbsoluteExpiration_Attribute
{
    [CachingConfiguration( AbsoluteExpiration = 60 )]
    public class PricingService
    {
        [Cache]
        public decimal GetProductPrice( string productId ) => throw new NotImplementedException();

        [Cache( AbsoluteExpiration = 20 )]
        public string[] GetProducts( string productId ) => throw new NotImplementedException();
    }
}