// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.DeepClone
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