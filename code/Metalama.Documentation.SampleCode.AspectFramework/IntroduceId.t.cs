using System;

namespace Doc.IntroduceId
{
    [IntroduceId]
    class MyClass
    {
        public Guid Id { get; } = Guid.NewGuid()
    }
}
