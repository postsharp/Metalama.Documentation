using System;
using Metalama.Patterns.Contracts;
namespace Doc.NumericContracts;
public class OrderLine
{
  private readonly decimal _nominalPrice;
  [Positive]
  public decimal NominalPrice
  {
    get
    {
      return _nominalPrice;
    }
    private init
    {
      if (value is < 0M)
      {
        throw new ArgumentOutOfRangeException("value", "The 'NominalPrice' property must be greater than or equal to 0.");
      }
      _nominalPrice = value;
    }
  }
  private readonly decimal _quantity;
  [StrictlyPositive]
  public decimal Quantity
  {
    get
    {
      return _quantity;
    }
    private init
    {
      if (value is <= 0M)
      {
        throw new ArgumentOutOfRangeException("value", "The 'Quantity' property must be strictly greater than 0.");
      }
      _quantity = value;
    }
  }
  private readonly int _discount;
  [Range(0, 100)]
  public int Discount
  {
    get
    {
      return _discount;
    }
    private init
    {
      if (value is < 0 or > 100)
      {
        throw new ArgumentOutOfRangeException("The 'Discount' property must be in the range [0, 100].", "value");
      }
      _discount = value;
    }
  }
}