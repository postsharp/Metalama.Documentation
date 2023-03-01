using System;

namespace Doc.NameDirect
{
    public class NameDemo
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [NameDirect.Name]
        public string? FullName
        {
            get
            {
                return (string?)(FirstName + " " + LastName);
            }

            private init
            {
                throw new NotSupportedException("You can't set the fullname");
            }
        }
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