using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc
{
    public class NameAttribute: OverrideFieldOrPropertyAspect
    {
       
        public override dynamic? OverrideProperty 
        {
            get => meta.This.FirstName + " " + meta.This.LastName;
            set => throw new NotSupportedException("You can't set the fullname");
        }
    }
}
