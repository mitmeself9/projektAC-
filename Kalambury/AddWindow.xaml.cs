using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Kalambury
{

    public partial class AddWindow : Window
    {

        public AddWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var NewPasswordCollection = new ObservableCollection<Passwordclass>();
            var PasswordModel = new Passwordclass();
            var PasswordXml = new PasswordsXml();
            NewPasswordCollection = PasswordXml.LoadDataFromXml();

            if (this.newName.Text.ToString() == String.Empty || this.newCategory.Text.ToString() == String.Empty)
                MessageBox.Show("Żadne pole nie może być puste!...");
            else
            {
                bool IfExist = PasswordModel.SearchDataInCollection(NewPasswordCollection, this.newName.Text.ToString(), this.newCategory.Text.ToString());

                if (IfExist == true)
                    MessageBox.Show("Przykro mi, takie hasło jest już w bazie...");
                else
                {
                    int lastId = PasswordModel.GetLastIdIndexValue(NewPasswordCollection) + 1;
                    PasswordXml.AddDataToXml(lastId, this.newName.Text.ToString(), this.newCategory.Text.ToString(), this.newLevel.Text.ToString());
                    PasswordModel.Id = lastId;
                    PasswordModel.Category = this.newCategory.Text.ToString();
                    PasswordModel.Name = this.newName.Text.ToString();
                    PasswordModel.Level = this.newLevel.Text.ToString();

                    NewPasswordCollection.Add(PasswordModel);
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.PasswordCollection = NewPasswordCollection;
                    mainWindow.UpdateListView(mainWindow.PasswordCollection);
                    // this.Close();
                    //MessageBox.Show("Dodano nowe hasło!"); - optionally
                }
            }
        }
    }
}
