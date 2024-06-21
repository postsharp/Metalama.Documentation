namespace Doc.IntroduceNestedClass_Members;
[Builder]
internal class Material
{
  public string Name { get; }
  public double Density { get; }
  class Builder : object
  {
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
  }
}