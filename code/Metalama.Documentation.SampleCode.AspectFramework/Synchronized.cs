// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

namespace Metalama.Documentation.SampleCode.AspectFramework.Synchronized
{
    [Synchronized]
    internal class SynchronizedClass
    {
        private double _total;
        private int _samplesCount;

        public void AddSample( double sample )
        {
            this._samplesCount++;
            this._total += sample;
        }

        public void Reset()
        {
            this._total = 0;
            this._samplesCount = 0;
        }

        public double Average => this._samplesCount / this._total;
    }
}