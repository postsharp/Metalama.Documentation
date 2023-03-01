using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;


namespace Doc.Misspelt
{
    public class Name : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty
        {
            get
            {
                var firstName = meta.Target.Type.FieldsAndProperties
                                          .First(t => String.Compare(t.Name, "FirstName", true) == 0);
                var lastName = meta.Target.Type.FieldsAndProperties
                                          .First(t => String.Compare(t.Name, "LastName", true) == 0);
                return ExpressionFactory.Parse($"{firstName.Name} + \" \" + {lastName.Name}");
            }

            set => throw new NotSupportedException("You can't set the fullname");
        }
    }
}
