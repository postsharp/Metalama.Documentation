// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System.Diagnostics;

namespace Doc.AspectConfiguration_Provider
{
    [LogConfiguration( Category = "SomeClass" )]
    public class SomeClass
    {
        [Log]
        public void SomeMethod() { }
    }

    [LogConfiguration( Category = "SomeClass" )]
    public class ChildNamespace
    {
        public class SomeOtherClass
        {
            [Log( Level = TraceLevel.Warning )]
            public void SomeMethod() { }
        }
    }
}