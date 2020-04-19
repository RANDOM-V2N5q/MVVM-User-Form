using System;
using System.Runtime.Serialization;

namespace MVVM_UserForm.Model {
    [Serializable]
    public class Person : ISerializable {
        public String Name { get; set; }
        public String Surname { get; set; }
        public UInt32 Weight { get; set; }
        public UInt32 Age { get; set; }

        public Person() { }
        public Person( String name , String surname, UInt32 weight, UInt32 age ) {
            Name = name;
            Surname = surname;
            Weight = weight;
            Age = age;
        }
        public Person( SerializationInfo info, StreamingContext context ) {
            Name = (String)info.GetValue(nameof(Name), Name.GetType());
            Surname = (String)info.GetValue(nameof(Surname), Surname.GetType());
            Weight = (UInt32)info.GetValue(nameof(Weight), Weight.GetType());
            Age = (UInt32)info.GetValue(nameof(Age), Age.GetType());
        }


        public bool IsTheSame( Person person ) {
            if(Name != person.Name) { return false; }
            if(Surname != person.Surname) { return false; }
            if(Weight != person.Weight) { return false; }
            if(Age != person.Age) { return false; }

            return true;
        }
        override public String ToString() {
            return $"{Name} {Surname}\n" +
                   $"{Weight} kg\t{Age} lat";
        }
        public void GetObjectData( SerializationInfo info, StreamingContext context ) {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Surname), Surname);
            info.AddValue(nameof(Weight), Weight);
            info.AddValue(nameof(Age), Age);
        }
    }
}
