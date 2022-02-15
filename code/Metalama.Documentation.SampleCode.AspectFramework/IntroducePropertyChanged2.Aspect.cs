using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.ComponentModel;

namespace Doc.IntroducePropertyChanged2
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
                tags: new TagDictionary { ["event"] = eventBuilder } );
        }

        [Template] public event PropertyChangedEventHandler? PropertyChanged;

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