// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using System;

namespace Doc.ToStringWithSimpleToString
{
    [ToString]
    internal class MovingVertex
    {
        public double X;

        public double Y;

        public double DX;

        public double DY { get; set; }

        public double Velocity => Math.Sqrt( (this.DX * this.DX) + (this.DY * this.DY) );
    }
}