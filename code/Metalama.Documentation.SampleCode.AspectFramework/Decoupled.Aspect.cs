// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using System;
using System.Linq;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.Decoupled;

internal class LogAspect : IAspect<IMethod>, IAspect<IProperty>
{
    private readonly LogAttribute _attribute;

    public LogAspect( LogAttribute attribute )
    {
        this._attribute = attribute;
    }

    public void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override( nameof(this.MethodTemplate) );
    }

    public void BuildAspect( IAspectBuilder<IProperty> builder )
    {
        builder.OverrideAccessors( null, nameof(this.MethodTemplate) );
    }

    [Template]
    public dynamic? MethodTemplate()
    {
        Console.WriteLine( $"[{this._attribute.Category}] Executing {meta.Target.Method}" );

        return meta.Proceed();
    }
}