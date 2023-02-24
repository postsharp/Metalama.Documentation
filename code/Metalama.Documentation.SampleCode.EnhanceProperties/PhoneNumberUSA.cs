using System;

namespace Doc.PhoneNumber
{
    public class Customer
    {
        [PhoneNumberUSA]
        public string? HomePhone { get; set; }
        public string? Mobile { get; set; }
    }
    public class PhoneNumberDemo
    {
        public static void Main(string[] args)
        {
            Customer customer1 = new Customer();
            customer1.HomePhone = "1234567890";
            customer1.Mobile = "9234567810";
            Console.WriteLine($"Home phone is {customer1.HomePhone}");
        }

    }
}
