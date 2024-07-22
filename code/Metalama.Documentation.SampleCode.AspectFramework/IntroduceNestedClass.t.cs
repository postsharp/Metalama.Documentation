namespace Doc.IntroduceNestedClass;
[Builder]
internal class Material
{
  public string Name { get; }
  public double Density { get; }
  public class Builder
  {
  }
}
internal class Metal : Material
{
  public double MeltingPoint { get; }
  public double ElectricalConductivity { get; }
  public class Builder : Material.Builder
  {
  }
}