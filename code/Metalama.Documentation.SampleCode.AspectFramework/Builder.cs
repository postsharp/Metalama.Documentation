// This is public domain Metalama sample code.

using System.ComponentModel.DataAnnotations;

namespace Doc.Builder_;

[Builder]
internal class Material
{
    [Required]
    public string Name { get; }

    public double Density { get; }
}