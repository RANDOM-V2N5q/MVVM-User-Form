using System;
using System.ComponentModel;

namespace MVVM_UserForm.ViewModel.BaseClass {
    class ViewModelBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(params String [] properties ) {
            if(PropertyChanged != null) {
                foreach(String property in properties) {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        }
    }
}
