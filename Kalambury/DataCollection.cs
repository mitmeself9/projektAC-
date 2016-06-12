using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kalambury
{
    public class Passwordclass : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return this.id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string category;
        public string Category
        {
            get { return this.category; }
            set
            {
                if (this.category != value)
                {
                    this.category = value;
                    OnPropertyChanged("Category");
                }
            }
        }

        private string level;
        public string Level
        {
            get { return this.level; }
            set
            {
                if (this.level != value)
                {
                    this.level = value;
                    OnPropertyChanged("Level");
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Passwordclass() { }

        public Passwordclass(int id, string name, string category, string level)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Level = level;
        }

        public bool SearchDataInCollection(ObservableCollection<Passwordclass> PasswordCollection, string name, string category)
        {
            foreach (var item in PasswordCollection)
            {
                if(item.Name == name && item.Category == category)
                    return true;
            }
            return false;
        }

        public int GetLastIdIndexValue(ObservableCollection<Passwordclass> PasswordCollection)
        {
           int lastIndexValue = 0;

           if (PasswordCollection.Count == 0)
               return lastIndexValue;
           else
           {
               lastIndexValue = PasswordCollection.Max(y => y.Id);
           }        

            return lastIndexValue;
        }

        public ObservableCollection<Passwordclass> SortCollection(ObservableCollection<Passwordclass> PasswordCollection, string valueToSort, string TypeofSort)
        {

            try
            {
                //I could be better , but I had problem with Func<TSampleClass, TPropertyType> etc..
                switch (TypeofSort)
                {
                    case "up":
                        if (valueToSort == "NAZWA")
                        {
                            var PasswordsUp = PasswordCollection.OrderBy(x => x.Name);
                            PasswordCollection = new ObservableCollection<Passwordclass>(PasswordsUp);
                        }
                        else if (valueToSort == "KATEGORIA")
                        {
                            var PasswordsUp = PasswordCollection.OrderBy(x => x.Category);
                            PasswordCollection = new ObservableCollection<Passwordclass>(PasswordsUp);
                        }
                        else if (valueToSort == "POZIOM")
                        {
                            var PasswordsUp = PasswordCollection.OrderBy(x => x.Level);
                            PasswordCollection = new ObservableCollection<Passwordclass>(PasswordsUp);
                        }
                        else if (valueToSort == "ID")
                        {
                            var PasswordsUp = PasswordCollection.OrderBy(x => x.Id);
                            PasswordCollection = new ObservableCollection<Passwordclass>(PasswordsUp);
                        }
  
                        break;

                    case "down":

                        if (valueToSort == "NAZWA")
                        {
                            var PasswordsUp = PasswordCollection.OrderByDescending(x => x.Name);
                            PasswordCollection = new ObservableCollection<Passwordclass>(PasswordsUp);
                        }
                        else if (valueToSort == "KATEGORIA")
                        {
                            var PasswordsUp = PasswordCollection.OrderByDescending(x => x.Category);
                            PasswordCollection = new ObservableCollection<Passwordclass>(PasswordsUp);
                        }
                        else if (valueToSort == "POZIOM")
                        {
                            var PasswordsUp = PasswordCollection.OrderByDescending(x => x.Level);
                            PasswordCollection = new ObservableCollection<Passwordclass>(PasswordsUp);
                        }
                        else if (valueToSort == "ID")
                        {
                            var PasswordsUp = PasswordCollection.OrderByDescending(x => x.Id);
                            PasswordCollection = new ObservableCollection<Passwordclass>(PasswordsUp);
                        }
                        
                        break;

                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups...\n" + ex.Message);
            }

            return PasswordCollection;
        }

    }
}
