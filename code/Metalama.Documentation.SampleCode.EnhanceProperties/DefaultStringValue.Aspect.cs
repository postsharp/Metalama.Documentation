using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.DefaultStr
{
    internal class DefaultStringValue : OverrideFieldOrPropertyAspect
    {
        public string InitialValue = string.Empty;
        public override dynamic? OverrideProperty
        {
            get => meta.Proceed()??InitialValue;
            set => meta.Proceed();
        }
    }
}
