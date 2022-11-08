// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.AspectConfiguration
{
    // The project fabric configures the project at compile time.
    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Project.LoggingOptions().DefaultCategory = "MyCategory";

            // Adds the aspect to all members.
            amender.With( c => c.Types.SelectMany( t => t.Methods ) ).AddAspectIfEligible<LogAttribute>();
        }
    }

    // Some target code.
    public class SomeClass
    {
        public void SomeMethod() { }
    }
}