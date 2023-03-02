
namespace Doc.GenerateResetMethods
{
    [GenerateResetMethods]
    public class Foo
    {
        private int _x;

        public string Y { get; set; }

        public string Z => this.Y;
    }
}