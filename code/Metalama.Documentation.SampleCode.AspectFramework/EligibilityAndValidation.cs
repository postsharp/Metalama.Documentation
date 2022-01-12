// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

namespace Metalama.Documentation.SampleCode.AspectFramework.EligibilityAndValidation
{
    internal class SomeClass
    {
        private object? _logger;

        [Log]
        private void InstanceMethod() { }

        [Log]
        private static void StaticMethod() { }
    }
}