// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @OutputAllSyntaxTrees
#endif

namespace Doc.Memento;

[Memento]
public class Vehicle
{
    public string Name { get; }

    public decimal Payload { get; set; }

    public string Fuel { get; set; }
}