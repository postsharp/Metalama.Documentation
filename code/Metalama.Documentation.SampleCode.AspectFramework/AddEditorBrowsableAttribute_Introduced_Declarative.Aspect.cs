using Metalama.Framework.Aspects;
using System.ComponentModel;

namespace Doc.AddEditorBrowsableAttribute_Introduced_Declarative
{
    public class AddEditorHiddenFieldAttribute : TypeAspect
    {
        [Introduce]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int __HiddenField;
    }
}
