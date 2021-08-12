using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravela.Documentation.SampleCode.AspectFramework
{
    class EmptyOverrideFieldOrPropertyExample
    {


        private int _field1;


        public int _field
        {
            get
            {
                return this._field1;
            }

            set
            {
                this._field1 = value;
            }
        }

        private string _property;

        [EmptyOverrideFieldOrProperty]
        public string Property
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
