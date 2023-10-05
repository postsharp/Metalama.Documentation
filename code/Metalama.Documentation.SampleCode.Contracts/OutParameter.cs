// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;

namespace Doc.Contracts.OutParameter
{
    public interface ICustomerService
    {
        // Returns the name of a given customer or null if it cannot be found,
        // but never returns an empty string.
        bool TryGetCustomerName( int id, [NotEmpty] out string? name );
    }

    public class CustomerService : ICustomerService
    {
        public bool TryGetCustomerName( int id, out string? name )
        {
            if ( id == 1 )
            {
                name = "Orontes I the Bactrian";

                return true;
            }
            else
            {
                name = null;

                return false;
            }
        }
    }
}