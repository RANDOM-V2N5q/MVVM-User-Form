using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MVVM_UserForm.ViewModel {
    using Model;
    using BaseClass;
    using System.Windows.Media;

    class UserForm : ViewModelBase {
        private UserFormLogic logic = new UserFormLogic();

        private String name;
        private String surname;
        private UInt32 weight;
        private UInt32 age;
        private Boolean isModified;

        public String Name {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public String Surname {
            get { return surname; }
            set {
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        public UInt32 Weight {
            get { return weight; }
            set {
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        public UInt32 Age {
            get { return age; }
            set {
                age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
        public ObservableCollection<Person> Persons {
            get { return new ObservableCollection<Person>(logic.GetList()); }
        }
        public Int32 Selected { get; set; }

        public UserForm() {
            Name = "";
            Surname = "";
            Weight = 0;
            Age = 0;
            Selected = -1;
        }

        #region Ok
        private ICommand _ok = null;

        public ICommand OK {
            get {
                if(_ok == null) {
                    _ok = new RelayCommand(OkExecute, OkCanExecute);
                }

                return _ok;
            }
        }

        private void OkExecute( object arg ) {
            if(logic.AddPerson(Name, Surname, Weight, Age)) {
                OnPropertyChanged(nameof(Persons));
                Reset.Execute(arg);
                isModified = true;
            }
        }

        private Boolean OkCanExecute( object arg ) {
            if(Name == "") return false;
            if(Surname == "") return false;
            return true;
        }
        #endregion Ok

        #region Reset
        private ICommand _reset;

        public ICommand Reset {
            get {
                if(_reset == null) {
                    _reset = new RelayCommand(ResetExecute, ResetCanExecute);
                }

                return _reset;
            }
        }

        private void ResetExecute(object arg ) {
            Name = "";
            Surname = "";
            Weight = 0;
            Age = 0;
        }

        private Boolean ResetCanExecute( object arg ) {
            return true;
        }
        #endregion Reset

        #region Edit
        private ICommand _edit = null;

        public ICommand Edit {
            get {
                if(_edit == null) {
                    _edit = new RelayCommand(EditExecute, EditCanExecute);
                }

                return _edit;
            }
        }

        private void EditExecute(object arg ) {
            Person tmp = logic.EditPerson(Selected);
            Name = tmp.Name;
            Surname = tmp.Surname;
            Weight = tmp.Weight;
            Age = tmp.Age;
        }

        private Boolean EditCanExecute(object arg ) {
            if(Selected == -1) return false;
            return true;
        }
        #endregion Edit

        #region Delete
        private ICommand _delete = null;

        public ICommand Delete {
            get {
                if(_delete == null) {
                    _delete = new RelayCommand(DeleteExecute, DeleteCanExecute);
                }

                return _delete;
            }
        }

        private void DeleteExecute( object arg ) {
            MessageBoxResult result = MessageBox.Show($"Czy chcesz usunąć ten element?\n{Persons[Selected]}", "Info", MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes) {
                logic.RemovePerson(Selected);
                OnPropertyChanged(nameof(Persons));
                isModified = true;
            }
        }

        private Boolean DeleteCanExecute( object arg ) {
            if(Selected == -1) return false;
            return true;
        }
        #endregion Delete

        #region Save
        private ICommand _save = null;
        public ICommand Save {
            get {
                if(_save == null) {
                    _save = new RelayCommand(SaveExecute, SaveCanExecute);
                }

                return _save;
            }
        }
        private void SaveExecute( object arg ) {
            logic.SaveList();
            isModified = false;
        }
        private Boolean SaveCanExecute( object arg ) {
            return true;
        }
        #endregion Save

        #region Load
        private ICommand _load = null;
        public ICommand Load {
            get {
                if(_load == null) {
                    _load = new RelayCommand(LoadExecute, LoadCanExecute);
                }

                return _load;
            }
        }
        private void LoadExecute( object arg ) {
            if(isModified) {
                MessageBoxResult result = MessageBox.Show($"Zapisać bieżącą listę?", "Info", MessageBoxButton.YesNoCancel);

                if(result == MessageBoxResult.Yes) {
                    logic.SaveList();
                }
                else if(result == MessageBoxResult.No) {

                }
                else {
                    return;
                }
            }

            isModified = false;
            logic.LoadList();
            OnPropertyChanged(nameof(Persons));
        }
        private Boolean LoadCanExecute( object arg ) {
            return true;
        }
        #endregion Load

        #region Exit
        private ICommand _exit = null;
        public ICommand Exit {
            get {
                if(_exit == null) {
                    _exit = new RelayCommand(ExitExecute, ExitCanExecute);
                }

                return _exit;
            }
        }
        private void ExitExecute( object arg ) {
            if(isModified) {
                MessageBoxResult result = MessageBox.Show($"Zapisać przed wyjściem?", "Info", MessageBoxButton.YesNoCancel);

                if(result == MessageBoxResult.Yes) {
                    logic.SaveList();
                    Environment.Exit(0);
                }
                else if(result == MessageBoxResult.No) {
                    Environment.Exit(0);
                }
            }
            else {
                Environment.Exit(0);
            }
        }
        private Boolean ExitCanExecute( object arg ) {
            return true;
        }
        #endregion Exit
    }
}
