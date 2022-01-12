// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Metalama.Documentation.SampleCode.AspectFramework.TypeFabric_
{
    internal class MyClass
    {
        private void Method1() { }

        public void Method2() { }

        private class Fabric : TypeFabric
        {
            public override void AmendType( ITypeAmender amender )
            {
                // Adds logging aspect to all public methods.
                amender.WithTargetMembers( t => t.Methods.Where( m => m.Accessibility == Accessibility.Public ) )
                    .AddAspect<LogAttribute>();
            }
        }
    }
}