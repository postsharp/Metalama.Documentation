
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.Normalize
{
    internal class NormalizeAttribute : FieldOrPropertyAspect
    {
        public override void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
        {
            builder.Advice.Override( builder.Target, nameof(this.OverrideProperty) );
        }

        [Template]
        private string OverrideProperty
        {
            set => meta.Target.FieldOrProperty.Value = value?.Trim().ToLowerInvariant();
        }
    }
}