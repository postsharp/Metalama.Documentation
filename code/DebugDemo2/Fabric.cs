using Metalama.Documentation.QuickStart;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;


namespace DebugDemo
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender project)
        {
            //1. Get all the types
            var allPublicMethods = project.Outbound.SelectMany(p => p.Types)
                                             //2. Get all methods of all these types
                                             .SelectMany(t => t.Methods)
                                             //3. Find only the public ones  
                                             .Where(t => t.Accessibility == Accessibility.Public);
            AddLoggingAspect(allPublicMethods);
            AddRetryAspect(allPublicMethods);
        }
        public void AddLoggingAspect(IAspectReceiver<IMethod> methods) => 
                methods.AddAspectIfEligible<LogAttribute>();
        public void AddRetryAspect(IAspectReceiver<IMethod> methods)
        {
            methods
            //Additional filter on the public methods
            .Where(t => t.Name.StartsWith("Try"))
                .AddAspectIfEligible<RetryAttribute>();
        }

    }
}
