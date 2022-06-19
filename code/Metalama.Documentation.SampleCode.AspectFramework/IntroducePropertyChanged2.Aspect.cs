// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.ComponentModel;

namespace Doc.IntroducePropertyChanged2
{
    internal class IntroducePropertyChangedAspect : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            var propertyChangedEvent = builder.Advice.IntroduceEvent(
                    builder.Target,
                    nameof(this.PropertyChanged) )
                .Declaration;

            builder.Advice.IntroduceMethod(
                builder.Target,
                nameof(this.OnPropertyChanged),
                args: new { theEvent = propertyChangedEvent } );
        }

        [Template]
        public event PropertyChangedEventHandler? PropertyChanged;

        [Template]
        protected virtual void OnPropertyChanged( string propertyName, IEvent theEvent )
        {
            theEvent.Invokers.Final.Raise(
                meta.This,
                meta.This,
                new PropertyChangedEventArgs( propertyName ) );
        }
    }
}