// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;

namespace Doc.ThrowOnCall
{
    public class Foo
    {
        [ThrowOnCall]
        private static void OldImplementation()
        {
            Console.WriteLine("This is the older implementation");
        }
		public static void Main(string[] args)
		{
			OldImplementation();
		}
    }
}