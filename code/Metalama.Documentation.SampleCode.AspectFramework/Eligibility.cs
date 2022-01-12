// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;
using System.IO;

namespace Metalama.Documentation.SampleCode.AspectFramework.Eligibility
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