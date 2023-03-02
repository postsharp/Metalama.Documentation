namespace Doc.Logging
{
    public class Program
    {
        [Log]
        public static void SayHello(string name)
        {
            System.Console.WriteLine($"Hello {name}");
        }
        public static void Main(string[] args)
        {
            SayHello("Your Majesty");
        }
    }
}
