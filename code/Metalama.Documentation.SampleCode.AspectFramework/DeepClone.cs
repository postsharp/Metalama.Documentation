using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.DeepClone
{
    class ManuallyCloneable : ICloneable
    {
        public object Clone()
        {
            return new ManuallyCloneable();
        }
    }

    [DeepClone]
    class AutomaticallyCloneable
    {
        int _a;

        ManuallyCloneable? _b;

        AutomaticallyCloneable? _c;
    }

    class DerivedCloneable : AutomaticallyCloneable
    {
        string? _d;
    }

}
