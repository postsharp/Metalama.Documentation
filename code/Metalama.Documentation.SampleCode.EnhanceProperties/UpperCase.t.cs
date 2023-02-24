using System;

namespace Doc.UpperCase
{
    public class Shipment
    {
        private string? _from;
        [UpperCase]
        public string? From
        {
            get
            {
                return _from;
            }

            set
            {
                if (value != null)
                {
                    this._from = value.ToUpper();
                }
                else
                {
                    this._from = value;
                }
            }
        }

        private string? _to;
        [UpperCase]
        public string? To
        {
            get
            {
                return _to;
            }

            set
            {
                if (value != null)
                {
                    this._to = value.ToUpper();
                }
                else
                {
                    this._to = value;
                }
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