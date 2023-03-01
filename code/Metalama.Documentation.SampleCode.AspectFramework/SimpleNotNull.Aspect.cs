using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.SimpleNotNull
{
    
    public class NotNullAttribute : ContractAspect
    {
        public override void Validate( dynamic? value )
        {
            if ( value == null! )
            {
                throw new ArgumentNullException( nameof(value) );
            }
            
        }
    }
}
