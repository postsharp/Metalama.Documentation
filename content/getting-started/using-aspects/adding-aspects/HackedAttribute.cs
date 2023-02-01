using Metalama.Framework.Code;

namespace LogDemo
{
    [Inheritable]
    public class HackedAttribute : TypeAspect
    {
        public override void BuildAspect(IAspectBuilder<INamedType> builder)
        {
            foreach (var method in builder.Target.Methods)
            {
                builder.Advice.Override(method, nameof(this.MethodTemplate));
            }
        }

        [Template]
        private dynamic? MethodTemplate()
        {
            Console.WriteLine($"Hacked! {meta.Target.Method.Name}");
            return meta.Proceed();
        }
    }
}
