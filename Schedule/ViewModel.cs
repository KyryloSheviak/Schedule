using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace Schedule
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            /*
            this.data.Add("яяя", new ObservableCollection<Exsams> {
                new Exsams("Дужий", "ОС"),
                new Exsams("Годунов", "Web"),
                new Exsams("Желтухин", "Переферия"),
            });

            this.data.Add("ффф", new ObservableCollection<Exsams> {
                new Exsams("sdfdvf", "sdfg"),
                new Exsams("xcvcvxc", "sdfg"),
                new Exsams("sfsgdfbg", "sdfg"),
            });
            

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter("data.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, this.data);
                }
            }
            */

            string json = "";
            if (File.Exists("data.json"))
            {
                json = File.ReadAllText("data.json");
                this.data = JsonConvert.DeserializeObject<Dictionary<string, ObservableCollection<Exsams>>>(json);
            }
            else
            {
                MessageBox.Show("Возникла ошибка чтения файла!", "Ошибка!");
            }

        }

        public Dictionary<string, ObservableCollection<Exsams>> data = new Dictionary<string, ObservableCollection<Exsams>>();
        public IDictionary<string, ObservableCollection<Exsams>> Data
        {
            get { return this.data; }
        }

        private KeyValuePair<string, ObservableCollection<Exsams>>? selectedKey = null;
        public KeyValuePair<string, ObservableCollection<Exsams>>? SelectedKey
        {
            get { return this.selectedKey; }
            set
            {
                this.selectedKey = value;
                this.OnPropertyChanged("SelectedKey");
                this.OnPropertyChanged("SelectedValue");
            }
        }

        public ObservableCollection<Exsams> SelectedValue
        {
            get
            {
                if (null == this.SelectedKey)
                {
                    return null;
                }

                return this.data[this.SelectedKey.Value.Key];
            }
            set
            {
                this.data[this.SelectedKey.Value.Key] = value;
                this.OnPropertyChanged("SelectedValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            var eh = this.PropertyChanged;
            if (null != eh)
            {
                eh(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
