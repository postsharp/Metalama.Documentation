// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.PrintFieldValues
{
    internal class PrintFieldValuesAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            foreach ( var fieldOrProperty in meta.Target.Type.FieldsAndProperties )
            {
                if ( fieldOrProperty.IsAutoPropertyOrField )
                {
                    var value = fieldOrProperty.Invokers.Final.GetValue( fieldOrProperty.IsStatic ? null : meta.This );
                    Console.WriteLine( $"{fieldOrProperty.Name}={value}" );
                }
            }

            return meta.Proceed();
        }
    }
}