using Metalama.Framework.Aspects;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.UpperCase
{
    /// <summary>
    /// Changes the value of a string property to Upper Case
    /// </summary>
    public class UpperCaseAttribute : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty
        {
            get => meta.Proceed();
            set
            {
                //Get the value and set it with what's given 
                meta.Proceed();
                //Now change the case to UpperCase.
                meta.Target.Property.Value = meta.Target.Property.Value?.ToString().ToUpper();
            }
        }
    }
}
