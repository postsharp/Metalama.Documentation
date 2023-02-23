using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Doc.Money
{
    public class ToMoneyAttribute : OverrideFieldOrPropertyAspect
    {
        public string Culture { get; set; } = "en-US";
        public override dynamic? OverrideProperty
        {

            get => meta.Proceed();
            set
            {
                meta.Proceed();
                if (meta.Target.Property.Type.Is(SpecialType.String))
                {
                    var propVal = meta.Target.Property.Value;
                    if (Regex.Match(propVal, "[0-9.]+").Success)
                    {
                        meta.Target.Property.Value =
                            string.Format(new CultureInfo(Culture, false), "{0:c2}",
                            Convert.ToDecimal(propVal));
                    }
                }
            }
        }
    }
}
