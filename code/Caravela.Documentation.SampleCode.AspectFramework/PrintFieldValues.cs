namespace Caravela.Documentation.SampleCode.AspectFramework.PrintFieldValues
{
    internal class TargetCode
    {
        private readonly int _a;

        public string? B { get; set; }

        private static readonly int _c;

        [PrintFieldValues]
        public void Method()
        {

        }

    }
}
