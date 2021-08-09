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
        int a;

        ManuallyCloneable b;

        AutomaticallyCloneable c;


        public virtual AutomaticallyCloneable Clone()
        {
            var clone = (AutomaticallyCloneable)MemberwiseClone();
            clone.b = (ManuallyCloneable)b.Clone();
            clone.c = c.Clone();
            return clone;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }

    [DeepClone]
    class DerivedCloneable : AutomaticallyCloneable
    {
        string d;


        public override DerivedCloneable Clone()
        {
            var clone = (DerivedCloneable)base.Clone();
            return clone;
        }
    }

}
