using System;
using System.Linq;
using Caravela.Framework.Aspects;
using Caravela.Framework.Code;

namespace Caravela.Documentation.SampleCode.AspectFramework.DeepClone
{
    class DeepCloneAttribute : Attribute, IAspect<INamedType>
    {
        public void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            var typedMethod = builder.AdviceFactory.IntroduceMethod(
                builder.Target, 
                nameof(CloneImpl),
                whenExists:OverrideStrategy.Override );

            typedMethod.Name = "Clone";
            typedMethod.ReturnType = builder.Target;

            builder.AdviceFactory.ImplementInterface(
                builder.Target, 
                typeof(ICloneable),
                whenExists: OverrideStrategy.Ignore);
        }

        [Template(IsVirtual = true)]
        public virtual dynamic CloneImpl()
        {
            // Define a local variable of the same type as the target type.
            var clone = meta.Target.Type.DefaultValue();

            // TODO: access to meta.Target.Method.Invokers.Base does not work.
            if ( meta.Target.Method.Invokers.Base == null )
            {
                // Invoke base.MemberwiseClone().
                clone = meta.Cast( meta.Target.Type,  meta.Base.MemberwiseClone() );
            }
            else
            {
                // Invoke the base method.
                clone = meta.Target.Method.Invokers.Base.Invoke(meta.This);
            }

            // Select clonable fields.
            var clonableFields =
                meta.Target.Type.FieldsAndProperties.Where(
                    f => f.IsAutoPropertyOrField &&
                    (f.Type.Is(typeof(ICloneable)) || 
                    (f.Type is INamedType fieldNamedType && fieldNamedType.Aspects<DeepCloneAttribute>().Any())));

            foreach ( var field in clonableFields )
            {
                // Check if we have a public method 'Clone()'.
                var fieldType = (INamedType)field.Type;
                var cloneMethod = fieldType.Methods.OfExactSignature("Clone", 0, Array.Empty<IType>());

                if ( (cloneMethod != null && cloneMethod.Accessibility == Accessibility.Public) || fieldType.Aspects<DeepCloneAttribute>().Any() )
                {
                    // If yes, call the method without a cast.
                    field.Invokers.Base.SetValue( 
                        clone,
                        meta.Cast(fieldType, field.Invokers.Base.GetValue(meta.This).Clone()) );

                }
                else
                {
                    // If no, use the interface.
                    field.Invokers.Base.SetValue(
                        clone,
                        meta.Cast(fieldType, ((ICloneable)field.Invokers.Base.GetValue(meta.This)).Clone()));
                }
            }

            return clone;
        }

        [InterfaceMember( IsExplicit = true)]
        object Clone()
        {
            return meta.This.Clone();
        }
    }
}
