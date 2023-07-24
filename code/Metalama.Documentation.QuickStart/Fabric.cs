using Metalama.Documentation.QuickStart;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;


namespace Metalama.Documentation.QuickStart
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender amender)
        {
            //Locating all types 
            var allMethods = amender.Outbound.SelectMany
                            (p => p.Types).SelectMany(t => t.Methods);
            AddLoggingAspect(allMethods);
            AddRetryAspect(allMethods);
        }
        public void AddLoggingAspect(IAspectReceiver<IMethod> methods)
        {
            methods.Where(t => t.Accessibility == Accessibility.Public)
                   .AddAspectIfEligible<LogAttribute>();
        }
        public void AddRetryAspect(IAspectReceiver<IMethod> methods)
        {
            methods
            .Where(t => t.Accessibility == Accessibility.Public &&
                       t.Name.StartsWith("Try"))
                .AddAspectIfEligible<RetryAttribute>();
        }

    }
}
