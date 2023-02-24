using System;


namespace Doc.ThrowOnCall
{
    
    public class ThrowOnCallDemo
    {
        [ThrowOnCall]
        public static void SaveIdentityDetails()
        {
            //A sensitive method that should 
            //ideally only be called by the current user
            Console.WriteLine("Saving identity details...");
        }

        public static string GetMachineName() =>
             Environment.MachineName;
        

        public static void Main(string[] args)
        {
            if(GetMachineName() == "DEVPC1234")
                SaveIdentityDetails();
        }
    }
}
