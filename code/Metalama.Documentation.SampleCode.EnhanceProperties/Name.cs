using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.Name
{
    public class NameDemo
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Name]
        public string? FullName { get; }


    }
    public class DemoClass
    {
        public static void Main(string[] args)
        {
            NameDemo person = new NameDemo();
            person.FirstName = "John";
            person.LastName = "Doe";
            Console.WriteLine($"Full name is {person.FullName}");
        }
    }
    
}
