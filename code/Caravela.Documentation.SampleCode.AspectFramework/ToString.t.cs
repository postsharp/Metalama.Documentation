namespace Caravela.Documentation.SampleCode.AspectFramework.ToString
{
    [ToString]
    class TargetCode
    {
        int x;
        public string Y { get; set; }


        public override string ToString()
        {
            return $"{ TargetCode x={x}, Y={Y} }";
        }
    }
}
