// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.QuickStart;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugDemo
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            //Locating all types 
            var allTypes = amender.Outbound.SelectMany( p => p.Types );

            //Finding all public methods from all types
            var allPublicMethods = allTypes
                .SelectMany( t => t.Methods )
                .Where( z => z.Accessibility == Accessibility.Public );

            //Adding Log aspect 
            allPublicMethods.AddAspectIfEligible<LogAttribute>();
        }
    }
}