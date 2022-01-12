// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.UpdateMethod
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