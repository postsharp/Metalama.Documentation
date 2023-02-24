using System;

namespace Doc.Trimmed
{
    public class Details
    {
        [Trimmed]
        public string? Code { get; set; }
        public bool IsCodeLengthValid() 
        {
            if (Code != null)
                return Code.Length == 7;
            return false;
        }
    }
    
    public class TrimmedDemo
    {
        public static void Main(string[] args)
        {
            Details detail1 = new()
            {
                Code = "GW12345  "
            };
            Details detail2 = new()
            {
                Code = "XY123455"
            };
            var isCodeValid1 = detail1.IsCodeLengthValid();
            var isCodeValid2 = detail2.IsCodeLengthValid();
            Console.WriteLine($"Code '{detail1.Code}' is valid : {isCodeValid1}");
            Console.WriteLine($"Code '{detail2.Code}' is valid : {isCodeValid2}");
        }
    }
}
