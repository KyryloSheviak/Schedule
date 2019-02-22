using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Schedule.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            GetData();
        }

        /// <summary>
        /// Асинхронная загрузка данных
        /// </summary>
        async private void GetData()
        {
            await Task.Run(() => {
                string json = "";
                try
                {
                    json = File.ReadAllText("data.json");
                    this.Data = JsonConvert.DeserializeObject<Dictionary<string, ObservableCollection<Exsams>>>(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            });
        }

        public Dictionary<string, ObservableCollection<Exsams>> data = new Dictionary<string, ObservableCollection<Exsams>>();
        public Dictionary<string, ObservableCollection<Exsams>> Data
        {
            get { return this.data; }
            set
            {
                this.data = value;
                this.OnPropertyChanged("Data");
            }
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

        public ICommand CloseProgram => new RelayCommand(() => Environment.Exit(0));
        public ICommand MinimizedProgram => new RelayCommand(() => {
            var app = System.Windows.Application.Current.Windows[0];
            app.WindowState = WindowState.Minimized;
        });
    }
}