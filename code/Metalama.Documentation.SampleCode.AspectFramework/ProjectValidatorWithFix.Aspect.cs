using Metalama.Framework.Code;
using Metalama.Framework.CodeFixes;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc.ProjectValidatorWithFix
{
    class MyProjectFabric : ProjectFabric
    {
        static DiagnosticDefinition<IField> _warning = new DiagnosticDefinition<IField>( "MY001", Severity.Warning, "The field {0} must be private." );

        public override void AmendProject( IProjectAmender amender )
        {
            amender.WithTargetMembers(
                p => p.Types.SelectMany( t => t.Fields.Where( f => f.Accessibility != Accessibility.Private && f.Type.Is( typeof( TextWriter ) ) ) ) )
                .ReportDiagnostic( f => _warning.WithArguments( f ).WithCodeFixes( CodeFixFactory.ChangeAccessibility( f, Accessibility.Private ) ) );
            
        }
    }
}

