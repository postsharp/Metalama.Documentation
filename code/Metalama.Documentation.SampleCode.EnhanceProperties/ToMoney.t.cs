using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Doc.Money
{
    public class Costings
    {
        private string _cost1;
        [ToMoney]
        public string Cost1
        {
            get
            {
                return _cost1;
            }

            set
            {
                this._cost1 = value;
                var propVal = _cost1;
                if (Regex.Match(propVal, "[0-9.]+").Success)
                {
                    this._cost1 = string.Format(new CultureInfo("en-US", false), "{0:c2}", Convert.ToDecimal(propVal));
                }
            }
        }

        private string _cost2;
        [ToMoney(Culture = "es-ES")]
        public string Cost2
        {
            get
            {
                return _cost2;
            }

            set
            {
                this._cost2 = value;
                var propVal = _cost2;
                if (Regex.Match(propVal, "[0-9.]+").Success)
                {
                    this._cost2 = string.Format(new CultureInfo("es-ES", false), "{0:c2}", Convert.ToDecimal(propVal));
                }
            }
        }
    }

    public class ToMoneyDemo
    {
        public static void Main(string[] args)
        {
            Costings s = new Costings();
            s.Cost1 = "123456.435";
            s.Cost2 = "123456.560";
            Console.WriteLine(s.Cost1);
            Console.WriteLine(s.Cost2);
        }
    }
}