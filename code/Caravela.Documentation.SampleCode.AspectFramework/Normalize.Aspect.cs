using System;
using Caravela.Framework.Aspects;
using Caravela.Framework.Code;

namespace Caravela.Documentation.SampleCode.AspectFramework.Normalize
{
    class NormalizeAttribute : Attribute, IAspect<IFieldOrProperty>
    {
        public void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
        {
            builder.AdviceFactory.OverrideFieldOrProperty(builder.Target, nameof(this.OverrideProperty));
        }

        [Template]
        string OverrideProperty
        {
            set
            {

                // Bug #28802: Expression-bodied setter templates are ignored.
                // Bug #28803: Setting a value through meta.Target.FieldOrProperty.Value generates access to the current layer instead of the next one.

                meta.Target.FieldOrProperty.Value = value?.Trim().ToLowerInvariant();
            }
        }
    }
}
