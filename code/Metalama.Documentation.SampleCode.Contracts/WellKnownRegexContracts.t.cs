using System;
using Metalama.Patterns.Contracts;
namespace Doc.WellKnownRegexContracts
{
  public class Customer
  {
    private string? _phone;
    [Phone]
    public string? Phone
    {
      get
      {
        return this._phone;
      }
      set
      {
        var regex = ContractHelpers.PhoneRegex!;
        if (value != null && !regex.IsMatch(value!))
        {
          var regex_1 = regex;
          throw new ArgumentException("The 'Phone' property must be a valid phone number.", "value");
        }
        this._phone = value;
      }
    }
    private string? _email;
    [Email]
    public string? Email
    {
      get
      {
        return this._email;
      }
      set
      {
        var regex = ContractHelpers.EmailRegex!;
        if (value != null && !regex.IsMatch(value!))
        {
          var regex_1 = regex;
          throw new ArgumentException("The 'Email' property must be a valid email address.", "value");
        }
        this._email = value;
      }
    }
    private string? _profile;
    [Url]
    public string? Profile
    {
      get
      {
        return this._profile;
      }
      set
      {
        var regex = ContractHelpers.UrlRegex!;
        if (value != null && !regex.IsMatch(value!))
        {
          var regex_1 = regex;
          throw new ArgumentException("The 'Profile' property must be a valid URL.", "value");
        }
        this._profile = value;
      }
    }
  }
}