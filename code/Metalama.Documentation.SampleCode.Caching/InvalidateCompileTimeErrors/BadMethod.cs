// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.InvalidateCompileTimeErrors.BadMethod
{
    public sealed class ProductCatalogue
    {
        [Cache]
        public decimal GetPrice( string productId ) => throw new NotImplementedException();

        [InvalidateCache( "GetBadPrice" )]
        public void UpdatePrice( string productId, decimal price ) => throw new NotImplementedException();
    }
}