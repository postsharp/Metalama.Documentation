using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Metalama.Documentation.SampleCode.AspectFramework.Normalize
{
    internal class NormalizeAttribute : FieldOrPropertyAspect
    {
        public override void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
        {
            builder.Advices.OverrideFieldOrProperty(builder.Target, nameof(this.OverrideProperty));
        }

        [Template]
        private string OverrideProperty
        {
            set => meta.Target.FieldOrProperty.Value = value?.Trim().ToLowerInvariant();
        }
    }
}
