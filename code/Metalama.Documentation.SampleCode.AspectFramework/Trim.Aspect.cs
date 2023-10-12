// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.Trim
{
    internal class TrimAttribute : ContractAspect
    {
        public override void Validate( dynamic? value )
        {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            value = value?.Trim();
#pragma warning restore IDE0059 // Unnecessary assignment of a value
        }
    }
}