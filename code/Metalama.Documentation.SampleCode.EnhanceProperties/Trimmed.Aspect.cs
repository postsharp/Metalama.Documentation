using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.Trimmed
{
    public class TrimAttribute : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty 
        {
            get => meta.Proceed();
            set => meta.Target.FieldOrProperty.Value = value?.Trim();
        }
    }
}
