using System.ComponentModel;
namespace Doc.AddEditorBrowsableAttribute_Introduced_Programmatic;
[AddEditorHiddenFieldAttribute]
public class C
{
  public int NormalField;
  [EditorBrowsable(EditorBrowsableState.Never)]
  private int __HiddenField;
}