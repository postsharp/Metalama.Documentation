
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Metalama.Documentation.QuickStart.Fabrics
{

    internal class AddLoggingToBaseClassChildren : ProjectFabric
    {
        public override void AmendProject(IProjectAmender amender)
        {
            //Locate all derived types of a given base class
            amender.Outbound.SelectMany(t => t.GetDerivedTypes(typeof(BaseClass)))
                            //Find all methods of all of these types
                            .SelectMany(t => t.Methods)
                            //Find all the public member functions of these types
                            .Where(method => method.Accessibility == Accessibility.Public)
                            //Add `Log` attribute to all of these  
                            .AddAspectIfEligible<LogAttribute>();
        }
    }
}
