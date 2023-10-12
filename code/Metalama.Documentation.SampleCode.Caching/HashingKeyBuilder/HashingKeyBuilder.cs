// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.HashKeyBuilder
{
    public sealed class FileSystem
    {
        public int OperationCount { get; private set; }

        [Cache]
        public byte[] ReadAll( string path )
        {
            this.OperationCount++;

            Console.WriteLine( "Reading the whole file." );

            return new byte[100 + this.OperationCount];
        }
    }
}