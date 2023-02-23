using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Doc.Money
{
    public class Costings
    {
        [ToMoney]
        public string Cost1 { get; set; }

        [ToMoney(Culture = "es-ES")]
        public string Cost2 { get; set; }
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