using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.DefaultStr
{
    public class Customer
    {
        [DefaultStringValue(InitialValue = "Mr./Ms.")]
        public string Salutation { get; set; }

        [DefaultStringValue(InitialValue = "someone@somedomain.com")]
        public string Email { get; set; }

        [DefaultStringValue(InitialValue = "+1-123-4567-890")]
        public string Phone { get; set; }

        public string Name { get; set; }

    }
    public class DefaultStringDemo 
    {
        public static void Main(string[] args)
        {
            Customer customer1 = new Customer();
            customer1.Salutation = "Dr.";
            customer1.Name = "Sam";
            Customer customer2 = new Customer();
            customer2.Salutation = "Mrs.";
            customer2.Name = "Jennifer";
            //Fabricated email
            customer2.Email = "jenni10110@yahoo.com";

            Console.WriteLine("Customer #1 details");
            Console.WriteLine($"Name = {customer1.Salutation} {customer1.Name}");
            Console.WriteLine($"Email = {customer1.Email}");
            Console.WriteLine($"Phone = {customer1.Phone}");

            Console.WriteLine("Customer #2 details");
            Console.WriteLine($"Name = {customer2.Salutation} {customer2.Name}");
            Console.WriteLine($"Email = {customer2.Email}");
            Console.WriteLine($"Phone = {customer2.Phone}");
        }

    }
}
