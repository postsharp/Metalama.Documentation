// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

namespace Metalama.Documentation.SampleCode.AspectFramework.RegistryStorage
{
    [RegistryStorage( "Animals" )]
    internal class Animals
    {
        public int Turtles { get; set; }

        public int Cats { get; set; }

        public int All => this.Turtles + this.Cats;
    }
}