// Warning CS8602 on `phone`: `Dereference of a possibly null reference.`
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Doc.PhoneNumber
{
    public class Customer
    {
        private string _homePhone;
        [PhoneNumberUSA]
        public string HomePhone
        {
            get
            {
                return _homePhone;
            }
            set
            {
                this._homePhone = value;
                var phone = this._homePhone?.ToString();
                if (phone != null && Regex.Match(phone, "[0-9]{10}").Success)
                {
                    this._homePhone = phone.Insert(3, "-").Insert(7, "-");
                }
            }
        }
        public string Mobile { get; set; }
    }
    public class PhoneNumberDemo
    {
        public static void Main(string[] args)
        {
            Customer customer1 = new Customer();
            customer1.HomePhone = "+1234567890";
            customer1.Mobile = "9234567810";
            Console.WriteLine($"Home phone is {customer1.HomePhone}");
        }
    }
}