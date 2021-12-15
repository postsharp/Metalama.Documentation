using System;
using System.ComponentModel;
using Caravela.Framework.Aspects;
using Caravela.Framework.Code;

namespace Caravela.Documentation.SampleCode.AspectFramework.IntroducePropertyChanged2
{
    class IntroducePropertyChangedAspect : TypeAspect
    {
        public override void BuildAspect( IAspectBuilder<INamedType> builder )
        {
            var eventBuilder = builder.Advices.IntroduceEvent(
                builder.Target,
                nameof(PropertyChanged));

            builder.Advices.IntroduceMethod(
                builder.Target,
                nameof(OnPropertyChanged),
                tags: new () {  ["event"] = eventBuilder });
        }


        [Template]
        public event PropertyChangedEventHandler? PropertyChanged;

        [Template]
        protected virtual void OnPropertyChanged( string propertyName )
        {
            ((IEvent) meta.Tags["event"]!).Invokers.Final.Raise(
                meta.This, 
                meta.This, new PropertyChangedEventArgs(propertyName));
        }
    }
}
