namespace Doc
{
    internal class EmptyOverrideFieldOrPropertyExample
    {
        [EmptyOverrideFieldOrProperty] public int Field;

        [EmptyOverrideFieldOrProperty] public string? Property { get; set; }
    }
}