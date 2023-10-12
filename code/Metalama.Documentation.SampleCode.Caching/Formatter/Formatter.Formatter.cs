// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Flashtrace.Formatters;
using System.IO;

namespace Doc.Formatter
{
    internal class FileInfoFormatter : Formatter<FileInfo>
    {
        public FileInfoFormatter( IFormatterRepository repository ) : base( repository ) { }

        public override void Format( UnsafeStringBuilder stringBuilder, FileInfo? value ) => stringBuilder.Append( value?.FullName ?? "<null>" );
    }
}