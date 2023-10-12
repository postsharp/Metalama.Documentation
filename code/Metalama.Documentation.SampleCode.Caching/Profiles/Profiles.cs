// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.Profiles
{
    public sealed class ProductCatalogue
    {
        public int OperationCount { get; private set; }

        [Cache( ProfileName = "Hot" )]
        public decimal GetPrice( string productId )
        {
            Console.WriteLine( "Getting the price from database." );
            this.OperationCount++;

            return 100 + this.OperationCount;
        }

        [Cache]
        public string[] GetProducts()
        {
            Console.WriteLine( "Getting the product list from database." );

            this.OperationCount++;

            return new[] { "corn" };
        }
    }
}