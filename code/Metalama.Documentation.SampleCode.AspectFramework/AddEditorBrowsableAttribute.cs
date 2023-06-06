using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.AddEditorBrowsableAttribute
{
    [HideFieldsFromEditor]
    public class C
    {
        public int NormalField;
        public string? __HiddenField;
    }
}
