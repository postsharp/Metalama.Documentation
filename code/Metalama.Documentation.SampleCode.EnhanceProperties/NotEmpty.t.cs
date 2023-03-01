using System;

namespace Doc.NotEmpty
{
    public class NotEmptyDemo
    {
        public static string GetUser([NotEmpty] string? userId)
        {
            if (userId != null && userId?.GetType() == typeof(global::System.String) && userId?.ToString() == string.Empty)
            {
                throw new ArgumentException($"{nameof(userId)} is empty.");
            }

            return string.Empty;
        }

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(GetUser("Sam").Length);
                Console.WriteLine(GetUser(string.Empty).Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}