// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doc.InvalidateAspect
{
    public sealed class ProductCatalogue
    {
        private readonly Dictionary<string, decimal> _dbSimulator = new() { ["corn"] = 100 };

        public int DbOperationCount { get; private set; }

        [Cache]                                     /*<Cache>*/
        public decimal GetPrice( string productId ) /*</Cache>*/
        {
            Console.WriteLine( $"Getting the price of {productId} from database." );
            this.DbOperationCount++;

            return this._dbSimulator[productId];
        }

        [Cache]
        public string[] GetProducts()
        {
            Console.WriteLine( "Getting the product list from database." );

            this.DbOperationCount++;

            return this._dbSimulator.Keys.ToArray();
        }

        [InvalidateCache( nameof(GetProducts) )]                  /*<InvalidateCache>*/
        public void AddProduct( string productId, decimal price ) /*</InvalidateCache>*/
        {
            Console.WriteLine( $"Adding the product {productId}." );

            this.DbOperationCount++;
            this._dbSimulator.Add( productId, price );
        }

        [InvalidateCache( nameof(GetPrice) )]
        public void UpdatePrice( string productId, decimal price )
        {
            if ( !this._dbSimulator.ContainsKey( productId ) )
            {
                throw new KeyNotFoundException();
            }

            Console.WriteLine( $"Updating the price of {productId}." );

            this.DbOperationCount++;
            this._dbSimulator[productId] = price;
        }
    }
}