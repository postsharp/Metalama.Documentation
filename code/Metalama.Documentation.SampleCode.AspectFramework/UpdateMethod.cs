// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;

namespace Doc.UpdateMethod
{
    [UpdateMethod]
    internal class CityHunter
    {
        private int _x;

        public string? Y { get; private set; }

        public DateTime Z { get; }
    }

    internal class Program
    {
        private static void Main()
        {
            CityHunter ch = new();
#if METALAMA
            ch.Update(0, "1");
#endif
        }
    }
}