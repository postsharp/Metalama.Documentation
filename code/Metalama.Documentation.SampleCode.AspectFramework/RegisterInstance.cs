// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Doc.RegisterInstance
{
    [RegisterInstance]
    internal class DemoClass
    {
        public DemoClass() : base() {}
        public DemoClass(int i) : this() {}
        public DemoClass(string s) {}
    }
}