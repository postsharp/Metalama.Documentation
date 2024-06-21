using System.ComponentModel.DataAnnotations;
namespace Doc.Builder_;
[Builder]
internal class Material
{
  [Required]
  public string Name { get; }
  public double Density { get; }
  private Material(string Name, double Density)
  {
    this.Name = Name;
    this.Density = Density;
  }
  public class Builder : object
  {
    private Builder(string Name)
    {
      this.Name = Name;
    }
    private double _density;
    private double Density
    {
      get
      {
        return _density;
      }
      set
      {
        _density = value;
      }
    }
    private string _name = default !;
    private string Name
    {
      get
      {
        return _name;
      }
      set
      {
        _name = value;
      }
    }
    public Material Build(string Name, double Density)
    {
      return new Material(this.Name, this.Density)!;
    }
  }
}