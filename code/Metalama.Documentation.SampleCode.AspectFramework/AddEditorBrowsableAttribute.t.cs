using System.ComponentModel;
namespace Doc.AddEditorBrowsableAttribute
{
  [HideFieldsFromEditor]
  public class C
  {
    public int NormalField;
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string? __HiddenField;
  }
}