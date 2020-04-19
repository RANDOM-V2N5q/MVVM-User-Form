using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MVVM_UserForm.Model {
    class Archive {
        public static void SaveToFile( List<Person> player ) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if(saveFileDialog.ShowDialog() == DialogResult.OK) {
                using(Stream outputStream = saveFileDialog.OpenFile()) {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                    serializer.Serialize(outputStream, player);
                }
            }
        }

        public static List<Person> LoadFromFile() {
            List<Person> rslt = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                using(Stream inputStream = openFileDialog.OpenFile()) {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                    rslt = (List<Person>)serializer.Deserialize(inputStream);
                }
            }

            return rslt;
        }
    }
}
