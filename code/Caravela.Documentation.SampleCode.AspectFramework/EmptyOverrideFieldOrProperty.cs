using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravela.Documentation.SampleCode.AspectFramework
{
    class EmptyOverrideFieldOrPropertyExample
    {
        [EmptyOverrideFieldOrProperty]
        public int _field;

        [EmptyOverrideFieldOrProperty]
        public string Property { get; set; }
    }
}
