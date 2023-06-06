using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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