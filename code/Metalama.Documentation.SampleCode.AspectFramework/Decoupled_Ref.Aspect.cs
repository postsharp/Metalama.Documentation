// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using System;
using System.Linq;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.Decoupled_Ref;

internal class LogAspect : IAspect<IMethod>, IAspect<IProperty>
{
    private readonly IRef<IAttribute> _attribute;

    public LogAspect( IRef<IAttribute> attribute )
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
        var attribute = this._attribute.GetTarget();

        var category =
            (string?)
            attribute.NamedArguments
                .SingleOrDefault( n => n.Key == "Category" )
                .Value
                .Value ?? "default";

        Console.WriteLine( $"[{category}] Executing {meta.Target.Method}" );

        return meta.Proceed();
    }
}