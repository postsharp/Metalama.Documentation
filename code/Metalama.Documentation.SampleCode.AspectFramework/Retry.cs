// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using System;

namespace Doc.Retry
{
    internal class Foo
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