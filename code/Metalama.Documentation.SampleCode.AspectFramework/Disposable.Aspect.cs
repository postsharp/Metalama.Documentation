// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;

namespace Doc.Disposable
{
    internal class DisposableAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            builder.Advice.ImplementInterface(
                builder.Target,
                typeof(IDisposable),
                whenExists: OverrideStrategy.Ignore );
        }

        // Introduces a the `Dispose(bool)` protected method, which is NOT an interface member.
        [Introduce( Name = "Dispose", IsVirtual = true, WhenExists = OverrideStrategy.Override )]
        protected void DisposeImpl( bool disposing )
        {
            // Call the base method, if any.
            meta.Proceed();

            var disposableFields = meta.Target.Type.FieldsAndProperties
                .Where( x => x.Type.Is( typeof(IDisposable) ) && x.IsAutoPropertyOrField.GetValueOrDefault() );

            // Disposes the current field or property.
            foreach ( var field in disposableFields )
            {
                field.ToExpression().Value?.Dispose();
            }
        }

        // Implementation of IDisposable.Dispose.
        [InterfaceMember]
        public void Dispose()
        {
            meta.This.Dispose( true );
        }
    }
}