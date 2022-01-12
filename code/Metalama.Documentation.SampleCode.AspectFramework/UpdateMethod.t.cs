using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.UpdateMethod
{
    [UpdateMethod]
    internal class CityHunter
    {
        private int _x;

        public string? Y { get; private set; }

        public DateTime Z { get; }


        public void Update(int _x, string? Y)
        {
            this._x = _x;
            this.Y = Y;
        }
    }

    internal class Program
    {
        private static void Main()
        {
            CityHunter ch = new();
            ch.Update(0, "1");
        }
    }
}
