// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

namespace Doc.CustomSyntaxSerializer
{
    [MemberCountAspect]
    public class TargetClass
    {
        public void Method1() { }

        public void Method1( int a ) { }

        public void Method2() { }
    }
}