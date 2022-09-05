// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;

namespace Doc.DeepClone
{
    internal class ManuallyCloneable : ICloneable
    {
        public object Clone()
        {
            return new ManuallyCloneable();
        }
    }

    [DeepClone]
    internal class AutomaticallyCloneable
    {
        private int _a;
        private ManuallyCloneable? _b;
        private AutomaticallyCloneable? _c;
    }

    internal class DerivedCloneable : AutomaticallyCloneable
    {
        private string? _d;
    }
}