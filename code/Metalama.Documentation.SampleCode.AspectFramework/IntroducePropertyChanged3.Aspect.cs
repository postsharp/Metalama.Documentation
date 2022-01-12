using System.ComponentModel;
using Metalama.Framework.Aspects;

namespace Metalama.Documentation.SampleCode.AspectFramework.IntroducePropertyChanged3
{
    internal class IntroducePropertyChangedAspect : TypeAspect
    {
        [Introduce]
        public event PropertyChangedEventHandler? PropertyChanged;

        [Introduce]
        protected virtual void OnPropertyChanged( string propertyName )
        {
            meta.This.PropertyChanged?.Invoke(meta.This, new PropertyChangedEventArgs(propertyName));
        }
    }
}
