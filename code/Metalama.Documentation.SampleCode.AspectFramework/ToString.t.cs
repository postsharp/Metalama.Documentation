namespace Doc.ToString
{
    [ToString]
    internal class TargetCode
    {
        private int _x;

        public string? Y { get; set; }


        public override string ToString()
        {
            return $"{{ TargetCode _x={_x}, Y={Y} }}";
        }
    }
}