// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Doc.EligibilityAndValidation
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