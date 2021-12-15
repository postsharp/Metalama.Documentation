using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.ProjectFabric_
{
    class Class1
    {
        public void Method1()
        {
            Console.WriteLine($"Executing Metalama.Documentation.SampleCode.AspectFramework.ProjectFabric_.Class1.Method1().");
            try
            {
                Console.WriteLine("Inside Class1.Method1");
                return;
            }
            finally
            {
                Console.WriteLine($"Exiting Metalama.Documentation.SampleCode.AspectFramework.ProjectFabric_.Class1.Method1().");
            }
        }
        public void Method2()
        {
            Console.WriteLine($"Executing Metalama.Documentation.SampleCode.AspectFramework.ProjectFabric_.Class1.Method2().");
            try
            {
                Console.WriteLine("Inside Class1.Method2");
                return;
            }
            finally
            {
                Console.WriteLine($"Exiting Metalama.Documentation.SampleCode.AspectFramework.ProjectFabric_.Class1.Method2().");
            }
        }
    }

    class Class2
    {
        public void Method1()
        {
            Console.WriteLine($"Executing Metalama.Documentation.SampleCode.AspectFramework.ProjectFabric_.Class2.Method1().");
            try
            {
                Console.WriteLine("Inside Class2.Method1");
                return;
            }
            finally
            {
                Console.WriteLine($"Exiting Metalama.Documentation.SampleCode.AspectFramework.ProjectFabric_.Class2.Method1().");
            }
        }
        public void Method2()
        {
            Console.WriteLine($"Executing Metalama.Documentation.SampleCode.AspectFramework.ProjectFabric_.Class2.Method2().");
            try
            {
                Console.WriteLine("Inside Class2.Method2");
                return;
            }
            finally
            {
                Console.WriteLine($"Exiting Metalama.Documentation.SampleCode.AspectFramework.ProjectFabric_.Class2.Method2().");
            }
        }
    }
}
