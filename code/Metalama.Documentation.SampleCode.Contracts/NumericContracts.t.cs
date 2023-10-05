using System;
using Metalama.Patterns.Contracts;
namespace Doc.NumericContracts
{
  public class OrderLine
  {
    private readonly decimal _nominalPrice;
    [Positive]
    public decimal NominalPrice
    {
      get
      {
        return this._nominalPrice;
      }
      private init
      {
        if (value < 0M)
        {
          throw new ArgumentOutOfRangeException("value", "The 'NominalPrice' property must be greater than 0.");
        }
        this._nominalPrice = value;
      }
    }
    private readonly decimal _quantity;
    [StrictlyPositive]
    public decimal Quantity
    {
      get
      {
        return this._quantity;
      }
      private init
      {
        if (value < 0.0000000000000000000000000001M)
        {
          throw new ArgumentOutOfRangeException("value", "The 'Quantity' property must be strictly greater than 0.");
        }
        this._quantity = value;
      }
    }
    private readonly int _discount;
    [Range(0, 100)]
    public int Discount
    {
      get
      {
        return this._discount;
      }
      private init
      {
        if (value < 0 || value > 100)
        {
          throw new ArgumentOutOfRangeException("The 'Discount' property must be between 0 and 100.", "value");
        }
        this._discount = value;
      }
    }
  }
}