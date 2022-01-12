namespace Metalama.Documentation.SampleCode.AspectFramework.Normalize
{
    internal class TargetCode
    {


        private string? _property;
        [Normalize]
        public string? Property
        {
            get
            {
                return this.Property_Source;
            }

            set
            {
                this.Property_Source = value?.Trim().ToLowerInvariant();
            }
        }

        private string? Property_Source
        {
            get
            {
                return this._property;
            }

            set
            {
                this._property = value;
            }
        }
    }
}