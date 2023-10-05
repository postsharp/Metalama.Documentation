// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;

namespace Doc.StringLengthContract
{
    public class Customer
    {
        [StringLength( 12, 64 )]
        public string? Password { get; set; }
    }
}