using System;

namespace Doc.RegisterInstance
{

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine( "Allocate object." );
            AllocateObject();

            Console.WriteLine( "GC.Collect()" );                
            GC.Collect();

            PrintInstances();
        }

        private static void AllocateObject()
        {
            var o = new DemoClass();

            PrintInstances();
        }

        private static void PrintInstances()
        {
            foreach ( var instance in InstanceRegistry.GetInstances() )
            {
                Console.WriteLine( instance );
            }
        }
    }

 


}
