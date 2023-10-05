// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;

namespace Doc.Contracts.ReturnValue
{
    public interface ICustomerService
    {
        // Returns the name of a given customer or null if it cannot be found,
        // but never returns an empty string.
        [return: NotEmpty]
        public string? GetCustomerName( int id );
    }

    public class CustomerService : ICustomerService
    {
        public string? GetCustomerName( int id )
        {
            if ( id == 1 )
            {
                return "Orontes I the Bactrian";
            }
            else
            {
                return null;
            }
        }
    }
}