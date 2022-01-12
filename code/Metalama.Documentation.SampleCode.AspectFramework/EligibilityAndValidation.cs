namespace Metalama.Documentation.SampleCode.AspectFramework.EligibilityAndValidation
{
    internal class SomeClass
    {
        private object? _logger;

        [Log]
        private void InstanceMethod() { }


        [Log]
        private static void StaticMethod() { }
        
    }

}
