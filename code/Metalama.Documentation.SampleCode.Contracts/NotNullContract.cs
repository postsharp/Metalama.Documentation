// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;

namespace Doc.NotNullContract
{
    public class Instrument
    {
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public Category Category { get; set; }

        public Instrument( [NotNull] string name, [NotNull] Category category )
        {
            this.Name = name;
            this.Category = category;
        }
    }

    public class Category { }
}