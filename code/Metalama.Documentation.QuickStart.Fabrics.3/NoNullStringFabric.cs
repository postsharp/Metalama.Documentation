using Metalama.Documentation.QuickStart;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Code;

namespace DebugDemo2
{
    internal class AddNoNullsToPublicStringProperties : TypeFabric
    {
        public override void AmendType(ITypeAmender amender)
        {
            amender.Outbound.SelectMany(type => 
                                        type.FieldsAndProperties.Where(fieldOrProp => fieldOrProp.Accessibility == Accessibility.Public))
                            .Where(publicPropOrField => publicPropOrField.Type.Is(SpecialType.String) && publicPropOrField.IsRequired)
                            .AddAspectIfEligible<NoNullStringAttribute>(); 
        }
    }
}
