using System.ComponentModel;

namespace Doc.IntroducePropertyChanged2
{
    [IntroducePropertyChangedAspect]
    internal class TargetCode
    {


        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}