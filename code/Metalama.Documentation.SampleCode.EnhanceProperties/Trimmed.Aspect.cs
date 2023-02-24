using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.Trimmed
{
    public class TrimmedAttribute : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty 
        {
            get => meta.Proceed();
            set
            {
                if (value != null)
                {
                    meta.Target.FieldOrProperty.Value = value.Trim();
                }
            } 
        }
    }
}
