// This is public domain Metalama sample code.

using Metalama.Patterns.Observability;

namespace Doc.ChildObject;

[Observable]
public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

[Observable]
public class PersonViewModel
{
    public Person Person { get; set; }

    public PersonViewModel( Person model )
    {
        this.Person = model;
    }

    public string? FirstName => this.Person.FirstName;
    public string? LastName => this.Person.LastName;

    public string FullName => $"{this.FirstName} {this.LastName}";
}
