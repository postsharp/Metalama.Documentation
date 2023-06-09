using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using System.ComponentModel;

namespace Doc.AddEditorBrowsableAttribute_Introduced_Programmatic
{
    public class AddEditorHiddenFieldAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            var attribute = AttributeConstruction.Create(
               typeof( EditorBrowsableAttribute ),
                   new object[] { EditorBrowsableState.Never } );

            builder.Advice.IntroduceField( 
                builder.Target, 
                "__HiddenField",
                typeof( int ), 
                buildField: f => f.AddAttribute( attribute ) );
        }
    }
}
