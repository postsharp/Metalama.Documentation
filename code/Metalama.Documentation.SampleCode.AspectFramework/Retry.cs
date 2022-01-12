// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.Retry
{
    internal class TargetCode
    {
        [Retry]
        private void RetryDefault()
        {
            throw new InvalidOperationException();
        }

        [Retry( MaxAttempts = 10 )]
        private void RetryTenTimes()
        {
            throw new InvalidOperationException();
        }
    }
}