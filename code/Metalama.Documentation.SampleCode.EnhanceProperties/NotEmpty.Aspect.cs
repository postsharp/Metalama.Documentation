using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc
{
    public class NotEmptyAttribute : ContractAspect
    {
        public override void Validate(dynamic? value)
        {
            if(value != null 
                && value?.GetType() == typeof(string) 
                && value?.ToString() == string.Empty) 
            {
                throw new ArgumentException($"{nameof(value)} is empty.");
            }
        }
    }
}
