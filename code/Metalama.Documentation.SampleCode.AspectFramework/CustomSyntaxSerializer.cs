using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metalama.Documentation.SampleCode.AspectFramework.CustomSyntaxSerializer
{
    [MemberCountAspect]
    public class TargetClass
    {
        public void Method1() { }
        public void Method1(int a) { }
        public void Method2() { }

    }
}
