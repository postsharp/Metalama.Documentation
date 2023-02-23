using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Doc.PhoneNumber
{
    public class PhoneNumberUSAAttribute : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty
        {
            get => meta.Proceed();
            set
            {
                meta.Proceed();
                if (meta.Target.Property.Type.Is(SpecialType.String))
                {
                    var phone = meta.Target.Property.Value?.ToString();
                    if (phone != null && Regex.Match(phone, "[0-9]{10}").Success)
                    {
                        meta.Target.Property.Value = phone.Insert(3, "-").Insert(7, "-");
                    }
                }
            }
        }
    }
}
