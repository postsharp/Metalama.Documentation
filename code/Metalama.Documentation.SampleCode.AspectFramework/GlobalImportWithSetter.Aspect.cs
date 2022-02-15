using Metalama.Framework.Aspects;
using System;

namespace Doc.GlobalImportWithSetter
{
    internal class ImportAttribute : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty
        {
            get
            {
                // Gets the current value of the field or property.
                var service = meta.Proceed();

                if ( service == null )
                {
                    // Call the service provider.
                    service =
                        meta.Cast(
                            meta.Target.FieldOrProperty.Type,
                            ServiceLocator.ServiceProvider.GetService( meta.Target.Property.Type.ToType() ) );

                    // Set the field or property to the new value.
                    meta.Target.FieldOrProperty.Value = service;
                }

                return service;
            }

            set => throw new NotSupportedException();
        }
    }
}