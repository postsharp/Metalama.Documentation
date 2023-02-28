namespace Doc.SpecificLog
{
    public class SpecificLogDemo
    {
        [SpecificLog]
        public static void SayHello(string name)
        {
            System.Console.WriteLine($"Hello {name}");
        }
        public static void Main(string[] args)
        {
            SayHello("Gael");
        }
    }
}
