// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;

namespace Doc.NotNullFabric
{
    public class Instrument
    {
        public string Name { get; set; }

        public Category? Category { get; set; }

        public Instrument( string name, Category? category )
        {
            this.Name = name;
            this.Category = category;
        }
    }

    public class Category
    {
        // Internal APIs won't be checked by default.
        internal Category( string name ) 
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}