// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

namespace Doc.GenerateResetMethods
{
    [GenerateResetMethods]
    public class Foo
    {
        private int _x;

        public string Y { get; set; }

        public string Z => this.Y;
    }
}