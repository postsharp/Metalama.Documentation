namespace Metalama.Documentation.SampleCode.AspectFramework
{
    internal class EmptyOverrideFieldOrPropertyExample
    {
        [EmptyOverrideFieldOrProperty]
        public int Field;

        [EmptyOverrideFieldOrProperty]
        public string? Property { get; set; }
    }
}
