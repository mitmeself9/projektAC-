using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Kalambury
{
    public partial class FindWindow : Window
    {
        public FindWindow()
        {
            InitializeComponent();
        }

        bool _NameState  = false;

        public bool NameState
        {
            get { return _NameState; }
            set { _NameState = value; }
        }

        bool _CategoryState = false;

        public bool CategoryState
        {
            get { return _CategoryState; }
            set { _CategoryState = value; }
        }

        bool _LevelState = false;

        public bool LevelState
        {
            get { return _LevelState; }
            set { _LevelState = value; }
        }

        private void Name_Checked(object sender, RoutedEventArgs e)
        {
            this.NameFind.Visibility = Visibility.Visible;
            NameState = true;
            
        }

        private void Category_Checked(object sender, RoutedEventArgs e)
        {
            this.CategoryFind.Visibility = Visibility.Visible;
            CategoryState = true;
            
        }

        private void Level_Checked(object sender, RoutedEventArgs e)
        {
            this.LevelFind.Visibility = Visibility.Visible;
            LevelState = true;
        }

        private void Name_Unchecked(object sender, RoutedEventArgs e)
        {
            this.NameFind.Visibility = Visibility.Collapsed;
            NameState = false;
        }

        private void Category_Unchecked(object sender, RoutedEventArgs e)
        {
            this.CategoryFind.Visibility = Visibility.Collapsed;
            CategoryState = false;
        }

        private void Level_Unchecked(object sender, RoutedEventArgs e)
        {
            this.LevelFind.Visibility = Visibility.Collapsed;
            LevelState = false;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if ((NameState == true && this.NameTextBox.Text == String.Empty) ||
                (CategoryState == true && this.CategoryTextBox.Text == String.Empty))
                MessageBox.Show("Uzupełnij pola po których chcesz szukać!");
            else
            {
                PasswordsXml newPasswordXml = new PasswordsXml();
                ObservableCollection<Passwordclass> FindedCollection = new ObservableCollection<Passwordclass>();
                List<string> ListOFParameters = new List<string>();

                if (NameState == true)
                {
                    ListOFParameters.Add(this.NameTextBox.Text);
                    ListOFParameters.Add("nazwa");
                }
 
                if (CategoryState == true)
                {
                    ListOFParameters.Add(this.CategoryTextBox.Text);
                    ListOFParameters.Add("kategoria");
                }

                if (LevelState == true)
                {
                    ListOFParameters.Add(this.LevelTextBox.Text.ToString());
                    ListOFParameters.Add("poziom");
                }

                if(ListOFParameters.Count == 2)
                    FindedCollection = newPasswordXml.LookForDataInXml(ListOFParameters[0], ListOFParameters[1]);

                else if(ListOFParameters.Count == 4)
                    FindedCollection = newPasswordXml.LookForDataInXml(ListOFParameters[0], ListOFParameters[1],
                        ListOFParameters[2], ListOFParameters[3]);

                else if (ListOFParameters.Count == 6)
                    FindedCollection = newPasswordXml.LookForDataInXml(ListOFParameters[0], ListOFParameters[1],
                        ListOFParameters[2], ListOFParameters[3], ListOFParameters[4], ListOFParameters[5]);
              
                if (FindedCollection.Count > 0)
                {
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.PasswordCollection = FindedCollection;
                    mainWindow.UpdateListView(mainWindow.PasswordCollection);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nie ma takiego rejestru w bazie...");
                }

            }
        }
    }
}
