using Metalama.Framework.Aspects;

namespace Doc.GlobalImport
{
    internal class ImportAttribute : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty
        {
            get => ServiceLocator.ServiceProvider.GetService( meta.Target.FieldOrProperty.Type.ToType() );

            set => meta.Proceed();
        }
    }
}