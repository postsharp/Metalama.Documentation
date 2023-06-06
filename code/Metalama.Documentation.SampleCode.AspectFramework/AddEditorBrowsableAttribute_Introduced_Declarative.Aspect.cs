using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.AddEditorBrowsableAttribute_Introduced_Declarative
{
    public class AddEditorHiddenFieldAttribute : TypeAspect
    {
        [Introduce]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int __HiddenField;
    }
}
