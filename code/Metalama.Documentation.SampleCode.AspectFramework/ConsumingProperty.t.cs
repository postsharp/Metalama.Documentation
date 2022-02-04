using Metalama.Framework.Aspects;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.ConsumingProperty
{
#pragma warning disable CS0067
    public class Log : OverrideMethodAspect
    {
        public string? Category { get; set; }

        public override dynamic? OverrideMethod() => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");

    }
#pragma warning restore CS0067
}