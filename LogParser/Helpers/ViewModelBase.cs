using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LogParser.Annotations;

namespace LogParser.Helpers
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isBusy = false;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                SetValue(ref _isBusy, value);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return;
            backingField = value;
            OnPropertyChanged(propertyName);
        }

    }
}