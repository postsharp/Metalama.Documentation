// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;

namespace Doc.Contracts.Inheritane
{
    public interface ICustomer
    {
        [Phone]
        string? Phone { get; set; }

        [Url]
        string? Url { get; set; }

        [Range( 1900, 2100 )]
        int? BirthYear { get; set; }

        [Required]
        string? FirstName { get; set; }

        [Required]
        string LastName { get; set; }
    }

    public class Customer : ICustomer
    {
        public string? Phone { get; set; }

        public string? Url { get; set; }

        public int? BirthYear { get; set; }

        public string? FirstName { get; set; }

        public string LastName { get; set; }
    }
}