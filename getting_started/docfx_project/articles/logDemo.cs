using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDemo
{
    public class Program
    {
       
        public static void Main(string[] args)
        {
            CheckDB();
            UpdateDB();
        }
        [Log]
        public static void CheckDB()
        {
            //A method to check your database
            Console.WriteLine("Checking database ...");
        }

        [Log]
        public static void UpdateDB()
        {
            //A method to update your database
            Console.WriteLine("Updating database...");
        }
    }
}
