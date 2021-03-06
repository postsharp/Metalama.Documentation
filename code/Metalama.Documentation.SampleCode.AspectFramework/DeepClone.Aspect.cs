// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using System;
using System.Linq;

namespace Doc.DeepClone
{
    [Inherited]
    public class DeepCloneAttribute : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            builder.Advice.IntroduceMethod(
                builder.Target,
                nameof(this.CloneImpl),
                whenExists: OverrideStrategy.Override,
                buildMethod: t =>
                {
                    t.Name = "Clone";
                    t.ReturnType = builder.Target;
                } );

            builder.Advice.ImplementInterface(
                builder.Target,
                typeof(ICloneable),
                whenExists: OverrideStrategy.Ignore );
        }

        [Template( IsVirtual = true )]
        public virtual dynamic CloneImpl()
        {
            // This compile-time variable will receive the expression representing the base call.
            // If we have a public Clone method, we will use it (this is the chaining pattern). Otherwise,
            // we will call MemberwiseClone (this is the initialization of the pattern).
            IExpression baseCall;

            if ( meta.Target.Method.IsOverride )
            {
                ExpressionFactory.Capture( meta.Base.Clone(), out baseCall );
            }
            else
            {
                ExpressionFactory.Capture( meta.Base.MemberwiseClone(), out baseCall );
            }

            // Define a local variable of the same type as the target type.
            var clone = meta.Cast( meta.Target.Type, baseCall )!;

            // Select clonable fields.
            var clonableFields =
                meta.Target.Type.FieldsAndProperties.Where(
                    f => f.IsAutoPropertyOrField &&
                         ((f.Type.Is( typeof(ICloneable) ) && f.Type.SpecialType != SpecialType.String) ||
                          (f.Type is INamedType fieldNamedType &&
                           fieldNamedType.Aspects<DeepCloneAttribute>().Any())) );

            foreach ( var field in clonableFields )
            {
                // Check if we have a public method 'Clone()' for the type of the field.
                var fieldType = (INamedType) field.Type;
                var cloneMethod = fieldType.Methods.OfExactSignature( "Clone", Array.Empty<IType>() );

                IExpression callClone;

                if ( cloneMethod is { Accessibility: Accessibility.Public } ||
                     fieldType.Aspects<DeepCloneAttribute>().Any() )
                {
                    // If yes, call the method without a cast.
                    ExpressionFactory.Capture( field.Invokers.Base!.GetValue( meta.This )?.Clone(), out callClone );
                }
                else
                {
                    // If no, explicitly cast to the interface.
                    ExpressionFactory.Capture( ((ICloneable) field.Invokers.Base!.GetValue( meta.This ))?.Clone(), out callClone );
                }

                if ( cloneMethod == null || !cloneMethod.ReturnType.ConstructNullable().Is( fieldType ) )
                {
                    // If necessary, cast the return value of Clone to the field type.
                    ExpressionFactory.Capture( meta.Cast( fieldType, callClone.Value )!, out callClone );
                }

                // Finally, set the field value.
                field.Invokers.Base!.SetValue( clone, callClone.Value );
            }

            return clone;
        }

        [InterfaceMember( IsExplicit = true )]
        private object Clone()
        {
            return meta.This.Clone();
        }
    }
}