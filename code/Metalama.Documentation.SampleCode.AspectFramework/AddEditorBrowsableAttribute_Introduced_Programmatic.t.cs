using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Doc.AddEditorBrowsableAttribute_Introduced_Programmatic
{
  [AddEditorHiddenFieldAttribute]
  public class C
  {
    public int NormalField;
    [EditorBrowsable(EditorBrowsableState.Never)]
    private int __HiddenField;
  }
}