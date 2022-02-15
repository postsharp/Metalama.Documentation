using Metalama.Framework.Aspects;

namespace Doc
{
#pragma warning disable CS0067
    public class EmptyOverrideMethodAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod() => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");

    }
#pragma warning restore CS0067
}