using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.MB 
{
    public class MBAttribute : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty 
        {
            get => meta.Proceed();
            set
            {
                meta.Proceed();

                //Assuming the value is set is in Raw bytes
                
                if (meta.Target.FieldOrProperty.Type.Is(SpecialType.Int64))
                    meta.Target.Property.Value = Convert.ToInt64(meta.Target.Property.Value / 1000000);
                if (meta.Target.FieldOrProperty.Type.Is(SpecialType.Int32))
                    meta.Target.Property.Value = Convert.ToInt32(meta.Target.Property.Value / 1000000);
                if (meta.Target.FieldOrProperty.Type.Is(SpecialType.Int16))
                    meta.Target.Property.Value = Convert.ToInt16(meta.Target.Property.Value / 1000000);
                if (meta.Target.FieldOrProperty.Type.Is(SpecialType.UInt16))
                    meta.Target.Property.Value = Convert.ToUInt16(meta.Target.Property.Value / 1000000);
                if (meta.Target.FieldOrProperty.Type.Is(SpecialType.UInt32))
                    meta.Target.Property.Value = Convert.ToUInt32(meta.Target.Property.Value / 1000000);
                if (meta.Target.FieldOrProperty.Type.Is(SpecialType.UInt64))
                    meta.Target.Property.Value = Convert.ToUInt64(meta.Target.Property.Value / 1000000);
            }
        }
    }
}
