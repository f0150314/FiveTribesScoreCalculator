using System;
using System.ComponentModel;

namespace Five_Tribes_Score_Calculator.Models
{
    public class BaseModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
