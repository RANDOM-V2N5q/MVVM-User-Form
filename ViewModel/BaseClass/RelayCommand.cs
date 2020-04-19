using System;
using System.Windows.Input;

namespace MVVM_UserForm.ViewModel.BaseClass {
    class RelayCommand : ICommand {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged {
            add {
                if(_canExecute != null) {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove {
                if(_canExecute != null) {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public RelayCommand( Action<object> execute, Predicate<object> canExecute ) {
            if(execute == null) {
                throw new ArgumentNullException(nameof(execute));
            }
            else {
                _execute = execute;
                _canExecute = canExecute;
            }
        }

        public bool CanExecute( object parameter ) {
            if(_canExecute != null) {
                return _canExecute(parameter);
            }
            else {
                return true;
            }
        }

        public void Execute( object parameter ) {
            _execute(parameter);
        }
    }
}
