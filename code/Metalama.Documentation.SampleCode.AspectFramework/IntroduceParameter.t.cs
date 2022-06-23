namespace Doc.IntroduceParameter
{


    class Foo
    {
        [RegisterInstance]
        public Foo(IInstanceRegistry instanceRegistry = default)
        {
            instanceRegistry.Register(this);
        }
    }

    class Bar : Foo
    {
        public Bar(IInstanceRegistry instanceRegistry = default) : base(instanceRegistry)
        {
        }
    }

}
