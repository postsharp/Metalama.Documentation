using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;

namespace Doc.NameDirect
{
    public class Name : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty
        {
            get => ExpressionFactory.Parse("this.FirstName + \" \" + this.LastName");
            set => throw new NotSupportedException("You can't set the fullname");
        }
    }
}
