using Metalama.Patterns.Observability;

namespace Doc.Simple;

[Observable]
public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string FullName => $"{this.FirstName} {this.LastName}";
}