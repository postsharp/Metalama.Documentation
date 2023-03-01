using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Doc.SimpleNotNull
{
    public class TheClass
    {
        private string _field;
        [NotNull]
        public string Field
        {
            get
            {
                return this._field;
            }
            set
            {
                if ( value == null! )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                this._field = value;
            }
        }
        private string _property;
        [NotNull]
        public string Property
        {
            get
            {
                return this._property;
            }
            set
            {
                if ( value == null! )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                this._property = value;
            }
        }
        public void Method( [NotNull] string parameter )
        {
            if ( parameter == null! )
            {
                throw new ArgumentNullException( nameof( parameter ) );
            }
        }
    }
}
