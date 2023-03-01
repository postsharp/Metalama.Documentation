using System;

namespace Doc.Misspelt
{
    public class NameDemo
    {
        public string? firstName { get; set; }
        public string? LastNAME { get; set; }

        [Doc.Misspelt.Name]
        public string? FullName { get; }


    }
    public class DemoClass
    {
        public static void Main(string[] args)
        {
            NameDemo person = new NameDemo();
            person.firstName = "John";
            person.LastNAME = "Doe";
            Console.WriteLine($"Full name is {person.FullName}");
        }
    }
}
