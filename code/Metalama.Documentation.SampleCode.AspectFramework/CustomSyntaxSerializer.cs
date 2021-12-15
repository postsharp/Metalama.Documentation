using Caravela.Framework.Aspects;
using Caravela.Framework.Code;
using Caravela.Framework.Code.SyntaxBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravela.Documentation.SampleCode.AspectFramework.CustomSyntaxSerializer
{
    [MemberCountAspect]
    public class TargetClass
    {
        public void Method1() { }
        public void Method1(int a) { }
        public void Method2() { }

    }
}
