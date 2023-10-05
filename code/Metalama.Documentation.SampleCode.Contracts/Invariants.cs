// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;

namespace Doc.Invariants
{
    public class Invoice
    {
        public decimal Amount { get; set; }

        [Range( 0, 100 )]
        public int DiscountPercent { get; set; }

        [Range( 0, 100 )]
        public decimal DiscountAmount { get; set; }

        public virtual decimal DiscountedAmount => (this.Amount * (100 - this.Amount) / 100m) - this.DiscountAmount;

        [Invariant]
        private void CheckDiscounts()
        {
            if ( this.DiscountedAmount < 0 )
            {
                throw new PostconditionViolationException( "The discounted amount cannot be negative." );
            }
        }
    }
}