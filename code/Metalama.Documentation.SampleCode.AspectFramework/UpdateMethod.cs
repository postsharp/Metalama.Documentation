using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.UpdateMethod
{
    [UpdateMethod]
    class CityHunter
    {
        int _x;

        public string? Y { get; private set; }

        public DateTime Z { get; }
    }

    class Program
    {
        static void Main()
        {
            CityHunter ch = new();
#if METALAMA
            ch.Update(0, "1");
#endif
        }
    }
}
