namespace Doc.AuxiliaryTemplate
{
    public class SelfCachedClass
    {
        [Cache]
        public int Add(int a, int b) => a + b;

        [CacheAndLog]
        public int Rmove( int a, int b ) => a - b;
    }
}
