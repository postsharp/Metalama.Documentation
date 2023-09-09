namespace Doc.AuxiliaryTemplate_Return
{
    public class SelfCachedClass
    {
        [Cache]
        public int Add(int a, int b) => a + b;
    }
}
