using System.Security;
using Metalama.Framework.Aspects;

namespace Doc.Authorize
{
    public class AuthorizeAttribute : OverrideMethodAspect
    {
        public override dynamic OverrideMethod()
        {
            var currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            if (currentUser == "MrAllmighty")
            {
                return meta.Proceed();
            }
            else
            {
                throw new SecurityException("This method can only be called by MrAllmighty.");
            }
        }
    }
}
