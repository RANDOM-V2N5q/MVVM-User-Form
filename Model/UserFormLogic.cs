using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace MVVM_UserForm.Model {
    class UserFormLogic {
        private List<Person> persons;
        private Int32 selected;

        public UserFormLogic() {
            persons = new List<Person>();
            selected = -1;
        }

        public List<Person> GetList() {
            return persons;
        }

        public Person EditPerson(Int32 idx) {
            selected = idx;
            return persons[idx];
        }

        public Boolean AddPerson( String name, String surname, UInt32 weight, UInt32 age ) {
            if(selected == -1) {
                Person tmp = new Person(name, surname, weight, age);
                if(IsOnList(tmp)) {
                    MessageBox.Show($"Element już jest na liście", "Info", MessageBoxButton.OK);
                    return false;
                }
                else {
                    persons.Add(tmp);
                    return true;
                }
            }
            else {
                persons[selected].Name = name;
                persons[selected].Surname = surname;
                persons[selected].Weight = weight;
                persons[selected].Age = age;
                selected = -1;
                return true;
            }
        }

        public void RemovePerson(Int32 idx ) {
            persons.RemoveAt(idx);
        }

        public Boolean IsOnList(Person person ) {
            foreach(Person p in persons) {
                if(person.IsTheSame(p)) {
                    return true;
                }
            }
            return false;
        }

        public void SaveList() {
            Archive.SaveToFile(persons);
        }

        public void LoadList() {
            persons = Archive.LoadFromFile();
        }
    }
}
