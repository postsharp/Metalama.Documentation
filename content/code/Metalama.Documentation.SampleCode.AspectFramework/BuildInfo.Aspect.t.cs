namespace Doc.IntroduceMethodCount
{
    [IntroduceMethodCount]
    class MyClass
    {
        private void Method1() { }
        private void Method2() { }


        private int _methodCount = 2;

        public int MethodCount
        {
            get
            {
                return this._methodCount;
            }
        }
    }
}