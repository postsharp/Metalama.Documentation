using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.IntroduceId
{
    internal class IntroduceIdAttribute : TypeAspect
    {
        [Introduce]
        public Guid Id { get; } = Guid.NewGuid();
    }
}
