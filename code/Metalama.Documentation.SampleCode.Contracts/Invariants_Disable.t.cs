using System;
using Metalama.Patterns.Contracts;
namespace Doc.Invariants_Disable
{
  namespace Invoicing
  {
    public class Invoice
    {
      public decimal Amount { get; set; }
      private int _discountPercent;
      [Range(0, 100)]
      public int DiscountPercent
      {
        get
        {
          return this._discountPercent;
        }
        set
        {
          if (value is < 0 or > 100)
          {
            throw new ArgumentOutOfRangeException("The 'DiscountPercent' property must be in the range [0, 100].", "value");
          }
          this._discountPercent = value;
        }
      }
      private decimal _discountAmount;
      [Range(0, 100)]
      public decimal DiscountAmount
      {
        get
        {
          return this._discountAmount;
        }
        set
        {
          if (value is < 0M or > 100M)
          {
            throw new ArgumentOutOfRangeException("The 'DiscountAmount' property must be in the range [0, 100].", "value");
          }
          this._discountAmount = value;
        }
      }
      public virtual decimal DiscountedAmount => (this.Amount * (100 - this.Amount) / 100m) - this.DiscountAmount;
      [Invariant]
      private void CheckDiscounts()
      {
        if (this.DiscountedAmount < 0)
        {
          throw new PostconditionViolationException("The discounted amount cannot be negative.");
        }
      }
    }
  }
  namespace Fulfillment
  {
    public class FulfillmentProcess
    {
      public bool IsStarted { get; set; }
      public bool IsCompleted { get; set; }
      [Invariant]
      private void CheckState()
      {
        if (this.IsCompleted && !this.IsStarted)
        {
          throw new PostconditionViolationException();
        }
      }
    }
  }
}