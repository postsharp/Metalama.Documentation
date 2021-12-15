using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metalama.Documentation.SampleCode.AspectFramework
{
    class EmptyOverrideFieldOrPropertyExample
    {
        [EmptyOverrideFieldOrProperty]
        public int Field;

        [EmptyOverrideFieldOrProperty]
        public string? Property { get; set; }
    }
}
