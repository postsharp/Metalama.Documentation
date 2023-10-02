// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Doc.AspectConfiguration_ProjectDefault
{
    public class SomeClass
    {
        [Log]
        public void SomeMethod() { }
    }

    namespace ChildNamespace
    {
        public class SomeOtherClass
        {
            [Log]
            public void SomeMethod() { }
        }
    }
}