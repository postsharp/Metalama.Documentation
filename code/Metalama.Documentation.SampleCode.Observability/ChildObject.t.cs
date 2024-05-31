using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.ChildObject;
[Observable]
public class Person : INotifyPropertyChanged
{
  private string? _firstName;
  public string? FirstName
  {
    get
    {
      return _firstName;
    }
    set
    {
      if (!object.ReferenceEquals(value, _firstName))
      {
        _firstName = value;
        OnPropertyChanged("FirstName");
      }
    }
  }
  private string? _lastName;
  public string? LastName
  {
    get
    {
      return _lastName;
    }
    set
    {
      if (!object.ReferenceEquals(value, _lastName))
      {
        _lastName = value;
        OnPropertyChanged("LastName");
      }
    }
  }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}
[Observable]
public class PersonViewModel : INotifyPropertyChanged
{
  private Person _person = default !;
  public Person Person
  {
    get
    {
      return _person;
    }
    set
    {
      if (!object.ReferenceEquals(value, _person))
      {
        var oldValue = _person;
        if (oldValue != null)
        {
          oldValue.PropertyChanged -= _handlePersonPropertyChanged;
        }
        _person = value;
        OnObservablePropertyChanged("Person", oldValue, (INotifyPropertyChanged? )value);
        OnPropertyChanged("FirstName");
        OnPropertyChanged("FullName");
        OnPropertyChanged("LastName");
        OnPropertyChanged("Person");
        SubscribeToPerson(value);
      }
    }
  }
  public PersonViewModel(Person model)
  {
    this.Person = model;
  }
  public string? FirstName => this.Person.FirstName;
  public string? LastName => this.Person.LastName;
  public string FullName => $"{this.FirstName} {this.LastName}";
  private PropertyChangedEventHandler? _handlePersonPropertyChanged;
  [ObservedExpressions("Person")]
  protected virtual void OnChildPropertyChanged(string parentPropertyPath, string propertyName)
  {
  }
  [ObservedExpressions("Person")]
  protected virtual void OnObservablePropertyChanged(string propertyPath, INotifyPropertyChanged? oldValue, INotifyPropertyChanged? newValue)
  {
  }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  private void SubscribeToPerson(Person value)
  {
    if (value != null)
    {
      _handlePersonPropertyChanged ??= HandlePropertyChanged;
      value.PropertyChanged += _handlePersonPropertyChanged;
    }
    void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
      {
        var propertyName = e.PropertyName;
        switch (propertyName)
        {
          case "FirstName":
            OnPropertyChanged("FirstName");
            OnPropertyChanged("FullName");
            OnChildPropertyChanged("Person", "FirstName");
            break;
          case "LastName":
            OnPropertyChanged("FullName");
            OnPropertyChanged("LastName");
            OnChildPropertyChanged("Person", "LastName");
            break;
          default:
            OnChildPropertyChanged("Person", propertyName);
            break;
        }
      }
    }
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}