using System;

namespace Doc.NotNull
{
    internal class Foo
    {
        public void Method1([NotNull] string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
        }

        public void Method2([NotNull] out string s)
        {
            s = null!;
            if (s == null)
            {
                throw new PostConditionFailedException($"'s' cannot be null when the method returns.");
            }
        }


        [return: NotNull]
        public string Method3()
        {
            string returnValue;
            returnValue = null!;
            goto __aspect_return_1;
        __aspect_return_1:
            if (returnValue == null)
            {
                throw new PostConditionFailedException($"'<return>' cannot be null when the method returns.");
            }

            return returnValue;
        }


        private string _property;

        [NotNull]
        public string Property
        {
            get
            {
                return this._property;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Property");
                }

                this._property = value;
            }
        }

    }

    public class PostConditionFailedException : Exception
    {
        public PostConditionFailedException(string message) : base(message) { }
    }

}
