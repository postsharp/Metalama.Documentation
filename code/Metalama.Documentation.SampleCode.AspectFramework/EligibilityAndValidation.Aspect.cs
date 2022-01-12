using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Eligibility;
using System.IO;
using System.Linq;

namespace Metalama.Documentation.SampleCode.AspectFramework.EligibilityAndValidation
{
    internal class LogAttribute : OverrideMethodAspect
    {
        private static readonly DiagnosticDefinition<INamedType> _error1 = new ("MY001", Severity.Error, "The type {0} must have a field named 'logger'.");
        private static readonly DiagnosticDefinition<IField> _error2 = new("MY002", Severity.Error, "The type of the field {0} must be 'TextWriter'.");

        public override void BuildEligibility(IEligibilityBuilder<IMethod> builder)
        {
            base.BuildEligibility(builder);

            // The aspect must not be offered to non-static methods because it uses a static field 'logger'.
            builder.MustBeNonStatic();
        }

        public override void BuildAspect(IAspectBuilder<IMethod> builder)
        {
            base.BuildAspect(builder);

            // Validate that the target file has a field named 'logger' of type TextWriter.
            var declaringType = builder.Target.DeclaringType;
            var loggerField = declaringType.Fields.OfName("logger").SingleOrDefault();
            if ( loggerField == null )
            {
                _error1.WithArguments( declaringType ).ReportTo( declaringType, builder.Diagnostics );
                builder.SkipAspect();
            }
            else if ( !loggerField.Type.Is(typeof(TextWriter))                )
            {
                _error2.WithArguments(loggerField).ReportTo( loggerField, builder.Diagnostics );
                builder.SkipAspect();
            }
        }

        public override dynamic? OverrideMethod()
        {
            meta.This.logger.WriteLine($"Executing {meta.Target.Method}");

            return meta.Proceed();
        }
    }
}
