using Metalama.Documentation.QuickStart;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Code;

namespace Metalama.Documentation.QuickStart.Fabrics
{
    public class AddNoNullsToPublicStringProperties : ProjectFabric
    {
        public override void AmendProject(IProjectAmender amender)
        {
            amender.Outbound.SelectMany(type => type.Types)
                            .SelectMany(z => z.FieldsAndProperties.Where(fieldOrProp => fieldOrProp.Accessibility == Accessibility.Public))
                            .Where(publicPropOrField => publicPropOrField.Type.Is(SpecialType.String))
                            .AddAspectIfEligible<NoNullStringAttribute>();
        }
    }
}