// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.ComponentModel;

namespace Metalama.Documentation.SampleCode.AspectFramework.IntroducePropertyChanged2
{
    internal class IntroducePropertyChangedAspect : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            var eventBuilder = builder.Advices.IntroduceEvent(
                builder.Target,
                nameof(this.PropertyChanged) );

            builder.Advices.IntroduceMethod(
                builder.Target,
                nameof(this.OnPropertyChanged),
                tags: new Framework.Aspects.Tags { ["event"] = eventBuilder } );
        }

        [Template]
        public event PropertyChangedEventHandler? PropertyChanged;

        [Template]
        protected virtual void OnPropertyChanged( string propertyName )
        {
            ((IEvent) meta.Tags["event"]!).Invokers.Final.Raise(
                meta.This,
                meta.This,
                new PropertyChangedEventArgs( propertyName ) );
        }
    }
}