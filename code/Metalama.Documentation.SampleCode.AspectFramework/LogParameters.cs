namespace Doc.LogParameters
{
    internal class TargetCode
    {
        [Log]
        private void VoidMethod( int a, out int b )
        {
            b = a;
        }

        [Log]
        private int IntMethod( int a )
        {
            return a;
        }
    }
}