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
       
        public override dynamic OverrideProperty 
        {
            get => meta.This.FirstName + " " + meta.This.LastName;
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
            set => throw new NotSupportedException("You can't set the fullname");
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        }
    
    }
}
