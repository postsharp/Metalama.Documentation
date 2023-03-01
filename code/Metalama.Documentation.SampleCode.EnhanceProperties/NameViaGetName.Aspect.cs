using System;
using Metalama.Framework.Aspects;

namespace Doc.ViaGetName
{
    public class Name : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty
        {
            get
            {
                //Completely rely on dedicated name server
                return meta.This.GetName();
            }
            set => throw new NotSupportedException("You can't set the fullname");
        }
    }
}
