using Caravela.Framework.Aspects;
using Caravela.Framework.Code;
using Caravela.Framework.Diagnostics;
using Caravela.Framework.Eligibility;
using System.IO;
using System.Linq;

namespace Caravela.Documentation.SampleCode.AspectFramework.EligibilityAndValidation
{
    internal class LogAttribute : OverrideMethodAspect
    {
        static DiagnosticDefinition<INamedType> _error1 = new ("MY001", Severity.Error, "The type {0} must have a field named 'logger'.");
        static DiagnosticDefinition<IField> _error2 = new("MY002", Severity.Error, "The type of the field {0} must be 'TextWriter'.");

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
            INamedType declaringType = builder.Target.DeclaringType;
            var loggerField = declaringType.Fields.OfName("logger").SingleOrDefault();
            if ( loggerField == null )
            {
                builder.Diagnostics.Report(declaringType, _error1, declaringType);
                builder.SkipAspect();
            }
            else if ( !loggerField.Type.Is(typeof(TextWriter))                )
            {
                builder.Diagnostics.Report(loggerField, _error2, loggerField);
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
