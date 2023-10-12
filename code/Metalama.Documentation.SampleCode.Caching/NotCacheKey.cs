// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.NotCacheKey
{
    public class PricingService
    {
        [Cache]
        public decimal GetProductPrice( string productId, [NotCacheKey] string? correlationId ) => throw new NotImplementedException();

        [Cache]
        public string[] GetProducts( string productId, [NotCacheKey] string? correlationId ) => throw new NotImplementedException();
    }
}