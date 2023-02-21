using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using Metalama.Documentation.QuickStart;


namespace DebugDemo3
{
    public class AddLogAspectInGivenNamespaceFabric : ProjectFabric
    {
        /// <summary>
        /// Amends the project by adding Log aspect 
        /// to many eligible methods inside given namespace.
        /// </summary>
        /// <param name="project"></param>
        public override void AmendProject(IProjectAmender project)
        {
            //Adding Log attribute to all mehtods of all types 
            //that are available inside "Outer.Inner" namespace 

            project.Outbound.SelectMany(t => t.GlobalNamespace
                                              .DescendantsAndSelf()
                                              .Where(z => z.FullName.StartsWith("Outer.Inner")))
                            .SelectMany(ns => ns.Types.SelectMany(t => t.Methods))
                            .AddAspectIfEligible<LogAttribute>();
        }
    }
}
