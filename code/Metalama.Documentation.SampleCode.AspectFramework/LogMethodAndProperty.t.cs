using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.LogMethodAndProperty
{
    internal class TargetCode
    {
        [Log]
        public int Method(int a, int b)
        {
            Console.WriteLine("Entering TargetCode.Method(int, int)");
            try
            {
                return a + b;
            }
            finally
            {
                Console.WriteLine(" Leaving TargetCode.Method(int, int)");
            }
        }


        private int _property;

        [Log]
        public int Property
        {
            get
            {
                return this._property;
            }

            set
            {
                Console.WriteLine("Assigning TargetCode.Property.set");
                this._property = value;
            }
        }


        private string? _field;

        [Log]
        public string? Field
        {
            get
            {
                return this._field;
            }

            set
            {
                Console.WriteLine("Assigning TargetCode.Field.set");
                this._field = value;
            }
        }
    }
}
