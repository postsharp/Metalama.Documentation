// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;

namespace Doc.WellKnownRegexContracts
{
    public class Customer
    {
        [Phone]
        public string? Phone { get; set; }

        [Email]
        public string? Email { get; set; }

        [Url]
        public string? Profile { get; set; }
    }
}