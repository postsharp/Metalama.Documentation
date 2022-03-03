namespace Doc.Normalize
{
    internal class TargetCode
    {
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

        private string? Property_Source { get; set; }
    }
}