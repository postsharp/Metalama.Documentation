using System;
using Metalama.Patterns.Contracts;
namespace Doc.Localize
{
  public class Client
  {
    private string? _telephone;
    [Phone]
    public string? Telephone
    {
      get
      {
        return this._telephone;
      }
      set
      {
        var regex = ContractHelpers.PhoneRegex!;
        if (value != null && !regex.IsMatch(value!))
        {
          var regex_1 = regex;
          throw new ArgumentException("La valeur doit être un numéro de téléphone correct.", "value");
        }
        this._telephone = value;
      }
    }
  }
}