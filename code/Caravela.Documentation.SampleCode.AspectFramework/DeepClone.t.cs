using System;

namespace Caravela.Documentation.SampleCode.AspectFramework.DeepClone
{
    class ManuallyCloneable : ICloneable
    {
        public object Clone()
        {
            return new ManuallyCloneable();
        }
    }

    [DeepClone]
    class AutomaticallyCloneable : ICloneable
    {
        int _a;

        ManuallyCloneable? _b;

        AutomaticallyCloneable? _c;


        public virtual AutomaticallyCloneable Clone()
        {
            var clone = (AutomaticallyCloneable)MemberwiseClone();
            clone._b = (ManuallyCloneable?)_b.Clone();
            clone._c = _c.Clone();
            return clone;
        }

        private AutomaticallyCloneable Clone_Source()
        {
            return default(AutomaticallyCloneable);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }

    [DeepClone]
    class DerivedCloneable : AutomaticallyCloneable
    {
        string? _d;


        public override DerivedCloneable Clone()
        {
            var clone = (DerivedCloneable)base.Clone();
            return clone;
        }
    }

}
