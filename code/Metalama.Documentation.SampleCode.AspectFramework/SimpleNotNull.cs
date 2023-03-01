using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.SimpleNotNull
{
    public class TheClass
    {
        [NotNull]
        public string Field = "Field";

        [NotNull]
        public string Property { get; set; } = "Property";

        public void Method( [NotNull] string parameter )
        {

        }
    }
    
}
