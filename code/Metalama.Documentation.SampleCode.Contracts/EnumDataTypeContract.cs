// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Contracts;
using System;

namespace Doc.EnumDataTypeContract
{
    public class Message
    {
        [EnumDataType( typeof(ConsoleColor) )]
        public string? Color { get; set; }
    }
}