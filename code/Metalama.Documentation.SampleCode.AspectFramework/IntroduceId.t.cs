using System;

namespace Doc.IntroduceId
{
    [IntroduceId]
    class MyClass
    {


        private Guid _id = Guid.NewGuid();

        public Guid Id
        {
            get
            {
                return this._id;
            }
        }
    }
}
