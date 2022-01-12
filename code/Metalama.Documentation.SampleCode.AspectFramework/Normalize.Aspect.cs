// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Metalama.Documentation.SampleCode.AspectFramework.Normalize
{
    internal class NormalizeAttribute : FieldOrPropertyAspect
    {
        public override void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
        {
            builder.Advices.OverrideFieldOrProperty( builder.Target, nameof(this.OverrideProperty) );
        }

        [Template]
        private string OverrideProperty
        {
            set => meta.Target.FieldOrProperty.Value = value?.Trim().ToLowerInvariant();
        }
    }
}