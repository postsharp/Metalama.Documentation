﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Options;
using Metalama.Framework.Project;
using System;
using System.Diagnostics;

namespace Doc.AspectConfiguration_Provider
{
    // Options for the [Log] aspects.
    public class LoggingOptions : IHierarchicalOptions<IMethod>, IHierarchicalOptions<INamedType>,
                                  IHierarchicalOptions<INamespace>, IHierarchicalOptions<ICompilation>
    {
        public string? Category { get; init; }

        public TraceLevel? Level { get; init; }

        public object ApplyChanges( object changes, in ApplyChangesContext context )
        {
            var other = (LoggingOptions) changes;

            return new LoggingOptions { Category = other.Category ?? this.Category, Level = other.Level ?? this.Level };
        }
    }
}