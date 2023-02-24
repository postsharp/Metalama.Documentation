using Metalama.Framework.Aspects;


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
                //Now change the case to UpperCase.
                if (value != null)
                {
                    meta.Target.Property.Value = value.ToUpper();
                }
                else
                {
                    meta.Target.Property.Value = value;
                }
            }
        }
    }
}
