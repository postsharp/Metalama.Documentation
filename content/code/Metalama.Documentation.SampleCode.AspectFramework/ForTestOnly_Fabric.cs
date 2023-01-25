// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Doc.ForTestOnly_Fabric.ValidatedNamespace;

namespace Doc.ForTestOnly_Fabric
{
    namespace ValidatedNamespace
    {
        public class MyService { }
    }

    internal class NormalClass
    {
        // Usage NOT allowed here.
        private MyService _service = new();
    }

    namespace Tests
    {
        internal class TestClass
        {
            // Usage allowed here because we are in the Tests namespace.
            private MyService _service = new();
        }
    }
}