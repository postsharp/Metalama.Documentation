using System;
using Metalama.Framework.Aspects;

namespace Doc.ThrowOnCall
{
    public class ThrowOnCallAttribute : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {
            var currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (currentUser == "gael")
            {
                return meta.Proceed();
            }
            else
            {
                throw new NotSupportedException($"This method can only be called by the current user : {currentUser}");
            }
        }
    }
}
