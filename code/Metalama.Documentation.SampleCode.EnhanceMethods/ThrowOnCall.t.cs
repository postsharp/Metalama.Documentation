using System;
using System.Security.Principal;
namespace Doc.ThrowOnCall
{
    public class ThrowOnCallDemo
    {
        [ThrowOnCall]
        public static void SaveIdentityDetails()
        {
            var currentUser = WindowsIdentity.GetCurrent().Name;
            if (currentUser == "gael")
            {
                //A sensitive method that should
                //ideally only be called by the current user
                Console.WriteLine("Saving identity details...");
                return;
            }
            else
            {
                throw new NotSupportedException($"This method can only be called by the current user : { currentUser }");
            }
        }
        public static string GetMachineName() => Environment.MachineName;
        public static void Main(string[] args)
        {
            if (GetMachineName() == "DEVPC1234")
                SaveIdentityDetails();
        }
    }
}