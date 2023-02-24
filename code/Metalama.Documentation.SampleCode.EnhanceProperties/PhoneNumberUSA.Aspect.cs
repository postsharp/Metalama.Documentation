using Metalama.Framework.Aspects;


namespace Doc.PhoneNumber
{
    public class PhoneNumberUSAAttribute : OverrideFieldOrPropertyAspect
    {
        public override dynamic? OverrideProperty
        {
            get => meta.Proceed();
            set
            {
                if (value != null)
                {
                    var areaCode = value.Substring(0, 3);
                    var part1 = value.Substring(3, 3);
                    var part2 = value.Substring(6);
                    meta.Target.Property.Value = $"({areaCode})-{part1}-{part2}";
                }
            }
        }
    }
}
