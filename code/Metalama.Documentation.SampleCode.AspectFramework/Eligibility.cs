// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using System;
using System.IO;

namespace Doc.Eligibility
{
    internal class SomeClass
    {
        private TextWriter _logger = Console.Out;

        [Log]
        private void InstanceMethod() { }

        [Log]
        private static void StaticMethod() { }
    }
}