namespace Doc
{
    internal class EmptyOverrideFieldOrPropertyExample
    {


        private int _field;

        [EmptyOverrideFieldOrProperty]
        public int Field
        {
            get
            {
                return this._field;
            }

            set
            {
                this._field = value;
            }
        }

        private string? _property;

        [EmptyOverrideFieldOrProperty]
        public string? Property
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