using System;
using System.Linq;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Microsoft.Win32;

namespace Metalama.Documentation.SampleCode.AspectFramework.RegistryStorage
{
    internal class RegistryStorageAttribute : TypeAspect
    {
        public string Key { get; }

        public RegistryStorageAttribute(string key)
        {
            this.Key = "HKEY_CURRENT_USER\\SOFTWARE\\Company\\Product\\" + key;
        }

        public override void BuildAspect(IAspectBuilder<INamedType> builder )
        {
            foreach ( var property in builder.Target.FieldsAndProperties.Where( p=> p.IsAutoPropertyOrField))
            {
                builder.Advices.OverrideFieldOrProperty( property, nameof(this.OverrideProperty));
            }
            
        }

        [Template]
        private dynamic? OverrideProperty
        {
            get
            {
                var type = meta.Target.FieldOrProperty.Type.ToType();
                var value = Registry.GetValue(this.Key, meta.Target.FieldOrProperty.Name, null);
                if (value != null)
                {
                    return Convert.ChangeType(value, type);
                }
                else
                {
                    return meta.Target.FieldOrProperty.Type.DefaultValue();
                }
            }

            set
            {
                var stringValue = Convert.ToString(value);
                Registry.SetValue(this.Key, meta.Target.FieldOrProperty.Name, stringValue);
                meta.Proceed();
            }
        }
    }
}
