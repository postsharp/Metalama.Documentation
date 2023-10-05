// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;

namespace Doc.NumericContracts
{
    public class OrderLine
    {
        [Positive]
        public decimal NominalPrice { get; }

        [StrictlyPositive]
        public decimal Quantity { get; }

        [Range( 0, 100 )]
        public int Discount { get; }
    }
}