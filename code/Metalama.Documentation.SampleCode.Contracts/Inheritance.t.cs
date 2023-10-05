// Warning LAMA5002 on `FirstName`: `The [Required] contract has been applied to 'ICustomer.FirstName', but its type is nullable.`
// Warning LAMA5002 on `FirstName`: `The [Required] contract has been applied to 'Customer.FirstName', but its type is nullable.`
using System;
using Metalama.Patterns.Contracts;
namespace Doc.Contracts.Inheritane
{
  public interface ICustomer
  {
    [Phone]
    string? Phone { get; set; }
    [Url]
    string? Url { get; set; }
    [Range(1900, 2100)]
    int? BirthYear { get; set; }
    [Required]
    string? FirstName { get; set; }
    [Required]
    string LastName { get; set; }
  }
  public class Customer : ICustomer
  {
    private string? _phone;
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
    private string? _url;
    public string? Url
    {
      get
      {
        return this._url;
      }
      set
      {
        var regex = ContractHelpers.UrlRegex!;
        if (value != null && !regex.IsMatch(value!))
        {
          var regex_1 = regex;
          throw new ArgumentException("The 'Url' property must be a valid URL.", "value");
        }
        this._url = value;
      }
    }
    private int? _birthYear;
    public int? BirthYear
    {
      get
      {
        return this._birthYear;
      }
      set
      {
        if (value.HasValue && (value < 1900 || value > 2100))
        {
          throw new ArgumentOutOfRangeException("The 'BirthYear' property must be between 1900 and 2100.", "value");
        }
        this._birthYear = value;
      }
    }
    private string? _firstName;
    public string? FirstName
    {
      get
      {
        return this._firstName;
      }
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          if (value == null !)
          {
            throw new ArgumentNullException("value", "The 'FirstName' property is required.");
          }
          else
          {
            throw new ArgumentOutOfRangeException("value", "The 'FirstName' property is required.");
          }
        }
        this._firstName = value;
      }
    }
    private string _lastName = default !;
    public string LastName
    {
      get
      {
        return this._lastName;
      }
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          if (value == null !)
          {
            throw new ArgumentNullException("value", "The 'LastName' property is required.");
          }
          else
          {
            throw new ArgumentOutOfRangeException("value", "The 'LastName' property is required.");
          }
        }
        this._lastName = value;
      }
    }
  }
}