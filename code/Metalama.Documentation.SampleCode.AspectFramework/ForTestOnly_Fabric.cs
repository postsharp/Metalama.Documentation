﻿using Doc.ForTestOnly_Fabric.ValidatedNamespace;

namespace Doc.ForTestOnly_Fabric
{
    namespace ValidatedNamespace
    {

        public class MyService
        {

        }
    }

    internal class NormalClass
    {
        // Usage NOT allowed here.
        private MyService service = new();
    }

    namespace Tests
    {
        internal class TestClass
        {
            // Usage allowed here because we are in the Tests namespace.
            private MyService service = new();
        }
    }
}