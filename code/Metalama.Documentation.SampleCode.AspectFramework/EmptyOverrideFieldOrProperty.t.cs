using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metalama.Documentation.SampleCode.AspectFramework
{
    class EmptyOverrideFieldOrPropertyExample
    {


        private int _field;


        public int Field
        {
            get
            {
                return this._field;
            }

            set
            {
                this._field = value;
            }
        }

        private string? _property;

        [EmptyOverrideFieldOrProperty]
        public string? Property
        {
            get
            {
                return this._property;
            }

            set
            {
                this._property = value;
            }
        }
    }
}
