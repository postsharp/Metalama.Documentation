using System.ComponentModel;
using Metalama.Framework.Aspects;

namespace Metalama.Documentation.SampleCode.AspectFramework.IntroducePropertyChanged1
{
    internal class IntroducePropertyChangedAspect : TypeAspect
    {
        [Introduce]
        public event PropertyChangedEventHandler? PropertyChanged;

        [Introduce]
        protected virtual void OnPropertyChanged( string propertyName )
        {
            this.PropertyChanged?.Invoke(meta.This, new PropertyChangedEventArgs(propertyName));
        }
    }
}
