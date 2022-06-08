using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace op_lab44.Core
{
    internal class CurrentObj : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
