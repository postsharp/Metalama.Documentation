using System;

namespace Doc.PhoneNumber
{
    public class Customer
    {
        private string? _homePhone;
        [PhoneNumberUSA]
        public string? HomePhone
        {
            get
            {
                return _homePhone;
            }

            set
            {
                if (value != null)
                {
                    var areaCode = value.Substring(0, 3);
                    var part1 = value.Substring(3, 3);
                    var part2 = value.Substring(6);
                    this._homePhone = $"({areaCode})-{part1}-{part2}";
                }
            }
        }

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