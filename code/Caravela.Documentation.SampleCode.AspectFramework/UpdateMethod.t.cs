using System;

namespace Caravela.Documentation.SampleCode.AspectFramework.UpdateMethod
{
    [UpdateMethod]
    class CityHunter
    {
        int _x;

        public string? Y { get; private set; }

        public DateTime Z { get; }


        public void Update(int _x, string? Y)
        {
            this._x = _x;
            this.Y = Y;
        }
    }

    class Program
    {
        static void Main()
        {
            CityHunter ch = new();
            ch.Update(0, "1");
        }
    }
}
