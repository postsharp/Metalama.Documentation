// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Patterns.Contracts;

namespace Doc.CustomRegexContract
{
    [RunTimeOrCompileTime]
    public class PasswordAttribute : RegularExpressionAttribute
    {
        public PasswordAttribute() : base( "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&#])[A-Za-z\\d@$!%*?&#]{8,20}$\n" ) { }
    }
}