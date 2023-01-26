// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;

namespace Doc.ToStringWithComplexCodeFix
{
    [ToString]
    internal class MovingVertex
    {
        public double X;

        public double Y;

        public double DX;

        public double DY { get; set; }

        [NotToString]
        public double Velocity => Math.Sqrt( (this.DX * this.DX) + (this.DY * this.DY) );
    }
}