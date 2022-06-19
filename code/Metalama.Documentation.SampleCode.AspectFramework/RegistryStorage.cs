// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

namespace Doc.RegistryStorage
{
    [RegistryStorage( "Animals" )]
    internal class Animals
    {
        public int Turtles { get; set; }

        public int Cats { get; set; }

        public int All => this.Turtles + this.Cats;
    }
}