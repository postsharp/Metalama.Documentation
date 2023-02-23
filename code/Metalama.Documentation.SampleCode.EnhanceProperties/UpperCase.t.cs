using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Doc.UpperCase
{
    public class Shipment
    {
        public string CustomerName { get; set; }
        private string _from;
        [UpperCase]
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                this._from = value;
                this._from = this._from?.ToString().ToUpper();
            }
        }
        private string _to;
        [UpperCase]
        public string To
        {
            get
            {
                return _to;
            }
            set
            {
                this._to = value;
                this._to = this._to?.ToString().ToUpper();
            }
        }
    }
    public class UpperCase
    {
        public static void Main(string[] args)
        {
            Shipment package = new Shipment();
            package.From = "lhr";
            package.To = "jfk";
            Console.WriteLine($"Package is booked from {package.From} to {package.To}");
        }
    }
}