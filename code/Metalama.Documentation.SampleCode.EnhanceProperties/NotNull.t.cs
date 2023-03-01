using System;

namespace Doc.NotNull
{
    public class NotNullDemo
    {
        public static string GetUser([NotNull] string? userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            return string.Empty;
        }

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(GetUser(null).Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}