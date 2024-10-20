﻿// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.EnumViewModel;

public class EnumViewModelAttribute : CompilationAspect
{
    private readonly Type _enumType;
    private readonly string _targetNamespace;

    public EnumViewModelAttribute( Type enumType, string targetNamespace )
    {
        this._enumType = enumType;
        this._targetNamespace = targetNamespace;
    }

    public override void BuildAspect( IAspectBuilder<ICompilation> builder )
    {
        var enumType = (INamedType) TypeFactory.GetType( this._enumType );

        // Get or create the namespace.
        var ns = builder.WithNamespace( this._targetNamespace );

        // Introduce the type.
        var viewModelType = ns.IntroduceClass( enumType.Name + "ViewModel" );

        // Introduce a field for the underlying value.
        var underlyingValueField = viewModelType.IntroduceField(
                "_value",
                enumType,
                buildField: f =>
                {
                    f.Accessibility = Accessibility.Private;
                    f.Writeability = Writeability.ConstructorOnly;
                } )
            .Declaration;

        // Introduce a constructor.
        viewModelType.IntroduceConstructor(
            nameof(this.ConstructorTemplate),
            buildConstructor:
            c =>
            {
                c.Accessibility = Accessibility.Public;
                c.AddParameter( "underlying", underlyingValueField.Type );
            },
            args: new { underlyingValueField } );

        // Introduce properties.
        foreach ( var enumMember in enumType.Fields.Where(
                     f => f.Accessibility == Accessibility.Public ) )
        {
            viewModelType.IntroduceProperty(
                "Is" + enumMember.Name,
                nameof(this.IsEnumValue),
                null,
                IntroductionScope.Instance,
                buildProperty: p =>
                {
                    p.Name = "Is" + enumMember.Name;
                },
                args: new { enumMember, underlyingValueField } );
        }
    }

    [Template]
    public void ConstructorTemplate( [CompileTime] IField underlyingValueField )
    {
        underlyingValueField.Value = meta.Target.Parameters[0].Value;
    }

    [Template]
    public bool IsEnumValue(
        [CompileTime] IField enumMember,
        [CompileTime] IField underlyingValueField )
        => underlyingValueField.Value == enumMember.Value;
}