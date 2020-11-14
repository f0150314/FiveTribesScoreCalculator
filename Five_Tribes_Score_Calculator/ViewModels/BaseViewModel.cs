using System;
using System.ComponentModel;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Initialize(object parameter) { }

        public virtual void Initialize(object parameter, object secondParameter) { }
    }
}
