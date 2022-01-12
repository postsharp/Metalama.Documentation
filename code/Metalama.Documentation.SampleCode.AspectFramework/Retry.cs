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

        [Retry(MaxAttempts = 10)]
        private void RetryTenTimes()
        {
            throw new InvalidOperationException();
        }
    }
}
