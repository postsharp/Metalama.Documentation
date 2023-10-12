// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching.Aspects;
using System;
using System.IO;

namespace Doc.Formatter
{
    public sealed class FileSystem
    {
        public int OperationCount { get; private set; }

        [Cache]
        public byte[] ReadAll( FileInfo file )
        {
            this.OperationCount++;

            Console.WriteLine( "Reading the whole file." );

            return new byte[100 + this.OperationCount];
        }
    }
}