namespace Doc.IntroduceNestedClass;
[Builder]
internal class Material
{
  public string Name { get; }
  public double Density { get; }
  class Builder : object
  {
  }
}