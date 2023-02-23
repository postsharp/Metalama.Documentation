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

        [UpperCase]
        public string From { get; set; }

        [UpperCase]
        public string To { get; set; }
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
