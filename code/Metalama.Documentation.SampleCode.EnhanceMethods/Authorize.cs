using System;


namespace Doc.Authorize
{
    
    public class Program
    {
        [Authorize]
        public static void SaveIdentityDetails()
        {
            // A sensitive method that should 
            // ideally only be called by the current user.
            Console.WriteLine("Saving identity details...");
        }

        public static void Main(string[] args)
        {
             SaveIdentityDetails();
        }
    }
}
