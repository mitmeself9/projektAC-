using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.ComponentModel;

namespace Kalambury
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        PasswordsXml Passworded = new PasswordsXml();
        public ObservableCollection<Passwordclass> PasswordCollection { get; set; }
        public ObservableCollection<Passwordclass> PasswordGameCollection { get; set; }

        public MainWindow()
        {

            InitializeComponent();

            PasswordCollection = new ObservableCollection<Passwordclass>();

            PasswordGameCollection = new ObservableCollection<Passwordclass>();

            PasswordCollection = Passworded.LoadDataFromXml();

            this.DataContext = this;

            this.PasswordListWiev.ItemsSource = PasswordCollection;

            this.PasswordsComboBox.ItemsSource = Enum.GetValues(typeof(PasswordAmount));
            this.PasswordsComboBox.SelectedIndex = 0;

            this.TimeComboBox.ItemsSource = Enum.GetValues(typeof(Time));
            this.TimeComboBox.SelectedIndex = 0;

            this.RoundComboBox.ItemsSource = Enum.GetValues(typeof(FullRounds));
            this.RoundComboBox.SelectedIndex = 0;

            this.Team1ComboBox.ItemsSource = Enum.GetValues(typeof(TeamAmount));
            this.Team1ComboBox.SelectedIndex = 0;

            this.Team2ComboBox.ItemsSource = Enum.GetValues(typeof(TeamAmount));
            this.Team2ComboBox.SelectedIndex = 0;

            this.ValueSortTypeComboBox.ItemsSource = Enum.GetValues(typeof(ValueTypeSort));
            this.ValueSortTypeComboBox.SelectedIndex = 0;

            Timer = new DispatcherTimer(DispatcherPriority.Normal);
            Timer.Interval = new TimeSpan(0,0,1);
            Timer.Tick += Timer_Tick;

            UpdateLayout();
        }

        void Timer_Tick(object sender, EventArgs e)
        {

            if (TimeForClock > 0)
            {
                if (TimeForClock <= 10)
                {
                    if (TimeForClock % 2 == 0)
                    {
                        this.TimerTextBlock.Foreground = Brushes.Red;
                    }
                    else
                    {
                        this.TimerTextBlock.Foreground = Brushes.White;
                    }
                    TimeForClock--;
                    this.TimerTextBlock.Text = string.Format("0{0}:0{1}", TimeForClock / 60, TimeForClock % 60);
                }
                else
                {
                    TimeForClock--;
                    this.TimerTextBlock.Text = string.Format("0{0}:{1}", TimeForClock / 60, TimeForClock % 60);
                }
            }
            else
            {
                Timer.Stop();
                GameIterations--;

                if (GameIterations <= 0)
                {
                    int result = Team2Score - Team1Score;
                    if (result > 0)
                    {
                        MessageBox.Show("Koniec gry!\n Wygrała drużyna: " + Team2Name);
                    }
                    else if (result == 0)
                    {
                        MessageBox.Show("Koniec gry!\n REMIS!");
                    }
                    else
                    {
                        MessageBox.Show("Koniec gry!\n Wygrała drużyna: " + Team1Name);
                    }

                    SetParametersDefault();
                }
                else 
                { 

                    if (TeamToPLay)
                    {
                        TeamLabelContent = Team1List.ElementAt(0);
                        PlayerLabelContent = Team1List.ElementAt(1);
                        Team1List.RemoveRange(0, 2);
                        TeamToPLay = team1;
                    }

                    else
                    {
                        TeamLabelContent = Team2List.ElementAt(0);
                        PlayerLabelContent = Team2List.ElementAt(1);
                        Team2List.RemoveRange(0, 2);
                        TeamToPLay = team2;
                    }

                    TimerTextBlock.Visibility = Visibility.Hidden;

                    StackReadyPanel.Visibility = Visibility.Visible;

                    PasswordAmountToQuess = (int)((PasswordAmount)this.PasswordsComboBox.SelectedItem);

                    playerReady = false;

                    MessageBox.Show("Koniec czasu! Nastepny gracz!");
                }
            }
        }

        public void UpdateListView(ObservableCollection<Passwordclass> PasswordUpdateCollection)
        {
            this.PasswordListWiev.ItemsSource = null;
            this.PasswordListWiev.Items.Clear();
            this.PasswordListWiev.ItemsSource = PasswordUpdateCollection;
            this.PasswordListWiev.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewGameShow(object sender, RoutedEventArgs e)
        {
            this.PasswordStack.Visibility = Visibility.Collapsed;
            this.NewGame.Visibility = Visibility.Visible;
        }

        private void PasswordsListShow(object sender, RoutedEventArgs e)
        {
            this.NewGame.Visibility = Visibility.Collapsed;
            this.PasswordStack.Visibility = Visibility.Visible;
        }

        private void AddNewPassword(object sender, RoutedEventArgs e)
        {
            AddWindow windowAdd = new AddWindow();
            windowAdd.Owner = this; 
            windowAdd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            windowAdd.Show();
        }

        private void FindData(object sender, RoutedEventArgs e)
        {
            FindWindow windowFind = new FindWindow();
            windowFind.Owner = this;
            windowFind.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            windowFind.Show();
        }

        private void ShowAllPasswords(object sender, RoutedEventArgs e)
        {
            PasswordCollection = Passworded.LoadDataFromXml();
            UpdateListView(PasswordCollection);
        }

        private void RemoveSelected(object sender, RoutedEventArgs e)
        {

            while (PasswordListWiev.SelectedItems.Count > 0)
 
            {
              
                Passwordclass item = (Passwordclass)PasswordListWiev.SelectedItem;

                PasswordCollection.Remove(PasswordCollection.Single(x => x.Id == item.Id));
                
                int id = 1;

                foreach (var Password in PasswordCollection)
                {
                    Password.Id = id;
                    id++;
                }

                Passworded.UpdateXmlData(PasswordCollection);
                
           }
        }

        string _TypeOfSort = default(string);

        public string TypeOfSort
        {
            get { return _TypeOfSort; }
            set { _TypeOfSort = value; }
        }

        private void RadioSortChecked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            TypeOfSort = radio.Tag.ToString();

        }

        public enum ValueTypeSort { NAZWA, KATEGORIA, POZIOM, ID}

        private List<string> Team1List = new List<string>();

        private List<string> Team2List = new List<string>();

        private void Sort(object sender, RoutedEventArgs e)
        {
            Passwordclass PasswordObject = new Passwordclass();
            PasswordCollection = PasswordObject.SortCollection(PasswordCollection, this.ValueSortTypeComboBox.SelectedItem.ToString(), _TypeOfSort);
            
            UpdateListView(PasswordCollection);
        }

        private void StartGameButton(object sender, RoutedEventArgs e)
        {
            PasswordCollection = Passworded.LoadDataFromXml();

            int MinPasswordAmount = (((int)((TeamAmount)this.Team1ComboBox.SelectedItem) 
                +  (int)((TeamAmount)this.Team2ComboBox.SelectedItem)) 
                * (int)((PasswordAmount)this.PasswordsComboBox.SelectedItem)
                * (int)((FullRounds)this.RoundComboBox.SelectedItem));

            GameIterations = MinPasswordAmount;

            if (this.Team1Name.Text == String.Empty || this.Team2Name.Text == String.Empty)
                MessageBox.Show("Uzupełnij nazwe drużyn!");

            else if(PasswordCollection.Count < MinPasswordAmount)
            {
                MessageBox.Show(" Brakuje haseł w bazie dla takiej konfiguracji\n Liczba brakujących haseł: "       
                + (MinPasswordAmount - PasswordCollection.Count).ToString()); 
            }

            else
            {
                Team1NameProp = this.Team1Name.Text.ToString().ToUpper() + ":";
                Team2NameProp = this.Team2Name.Text.ToString().ToUpper() + ":";
                string team1name = "DRUŻYNA  >> " + this.Team1Name.Text.ToString().ToUpper() + " << ZGADUJE";
                string team2name = "DRUŻYNA  >> " + this.Team2Name.Text.ToString().ToUpper() + " << ZGADUJE";
                string playerIndexed = String.Empty;
                Team1List.Clear();
                Team2List.Clear();

                for (int i = 0; i < (int)((FullRounds)this.RoundComboBox.SelectedItem); i++)
                {
                    for (int j = 0; j < (int)((TeamAmount)this.Team1ComboBox.SelectedItem); j++)
                    {
                        int x = j + 1;
                        playerIndexed = "GRACZ " + x.ToString() + " GOTOWY?";
                        Team1List.Add(team1name);
                        Team1List.Add(playerIndexed);
                    }
                }

                for (int i = 0; i < (int)((FullRounds)this.RoundComboBox.SelectedItem); i++)
                {
                    for (int j = 0; j < (int)((TeamAmount)this.Team2ComboBox.SelectedItem); j++)
                    {
                        int x = j + 1;
                        playerIndexed = "GRACZ " + x.ToString() + " GOTOWY?";
                        Team2List.Add(team2name);
                        Team2List.Add(playerIndexed);
                    }
                }

                TimeChoosen = (int)((TeamAmount)this.TimeComboBox.SelectedItem);
                PasswordCollection = Passworded.LoadDataFromXml();
                
                TeamLabelContent = Team1List.ElementAt(0);
                PlayerLabelContent = Team1List.ElementAt(1);

                Team1List.RemoveAt(0);
                Team1List.RemoveAt(0);

                TeamToPLay = false;

                playerReady = false;

                this.NewGameHeader.Visibility = Visibility.Collapsed;
                this.PasswordShowHeader.Visibility = Visibility.Collapsed;
                this.NewGame.Visibility = Visibility.Collapsed;
                this.GameStarted.Visibility = Visibility.Visible;
                
            }
        }

        private void SetParametersDefault()
        {
            this.GameStarted.Visibility = Visibility.Collapsed;
            this.NewGame.Visibility = Visibility.Visible;
            this.NewGameHeader.Visibility = Visibility.Visible;
            this.PasswordShowHeader.Visibility = Visibility.Visible;
            TimerTextBlock.Visibility = Visibility.Hidden;
            StackReadyPanel.Visibility = Visibility.Visible;
            Team1Score = 0;
            Team2Score = 0;
            Team1ScoreContent = "0 pkt";
            Team2ScoreContent = "0 pkt";
            PasswordLabelContent = String.Empty;
            CategoryLabelContent = String.Empty;
            TimeForClock = 0;
            Timer.Stop();

        }

        private void FinishGame(object sender, RoutedEventArgs e)
        {
            SetParametersDefault();
        }

        private void PlayerReadyButton(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            int passwordTake = (int)((PasswordAmount)this.PasswordsComboBox.SelectedItem);
            for (int i = 0; i < passwordTake; i++)
            {
                int elementIndex = rnd.Next(0, PasswordCollection.Count - 1);
                PasswordGameCollection.Add(PasswordCollection.ElementAt(elementIndex));
                PasswordCollection.RemoveAt(elementIndex);

            }

            Passwordclass PasswordList = new Passwordclass();
            PasswordList = PasswordGameCollection.ElementAt(0);
            PasswordGameCollection.RemoveAt(0);
            PasswordLabelContent = PasswordList.Name;
            CategoryLabelContent = PasswordList.Category;

            PasswordAmountToQuess = passwordTake;

            playerReady = true;

            StackReadyPanel.Visibility = Visibility.Hidden;
            TimerTextBlock.Visibility = Visibility.Visible;
            TimeForClock = TimeChoosen;
            Timer.Start();

        }

        private void PasswordGuessed(object sender, RoutedEventArgs e)
        {
            if (playerReady)
            {
 
                StackReadyPanel.Visibility = Visibility.Visible;

                // here is some problem (only in full screen mode) with refreshing points in label graphic structure 

                if (TeamToPLay && PasswordAmountToQuess > 0)
                {
                    Team2Score += 1;
                    Team2ScoreContent = Team2Score.ToString() + " pkt";
                    Team2ScoreLabel.Refresh();
                    
                }
                else if (!TeamToPLay && PasswordAmountToQuess > 0)
                {
                    Team1Score += 1;
                    Team1ScoreContent = Team1Score.ToString() + " pkt";
                    Team1ScoreLabel.Refresh();
                    
                }

                Task.Factory.StartNew(() =>
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => { Team1ScoreLabel.UpdateLayout(); }));

                });

                Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => { Team2ScoreLabel.UpdateLayout(); }));

                PasswordAmountToQuess--;

                GameIterations--;

                try
                {

                    if (PasswordAmountToQuess == 0 && GameIterations > 0)
                    {
                        Timer.Stop();
                        TimeForClock = TimeChoosen;

                        if (TeamToPLay)
                        {
                            TeamLabelContent = Team1List.ElementAt(0);
                            PlayerLabelContent = Team1List.ElementAt(1);
                            Team1List.RemoveRange(0, 2);
                            
                            TeamToPLay = team1;
                        }
                        else
                        {
                            TeamLabelContent = Team2List.ElementAt(0);
                            PlayerLabelContent = Team2List.ElementAt(1);
                            Team2List.RemoveRange(0, 2);
                            TeamToPLay = team2;
                        }

                        TimerTextBlock.Visibility = Visibility.Hidden;
                        PasswordAmountToQuess = (int)((PasswordAmount)this.PasswordsComboBox.SelectedItem);
                        MessageBox.Show("Następny gracz!");
                        playerReady = false;
                    }
                    else if (GameIterations > 0)
                    {
                        Passwordclass PasswordList = new Passwordclass();
                        PasswordList = PasswordGameCollection.ElementAt(0);
                        PasswordGameCollection.RemoveAt(0);
                        PasswordLabelContent = PasswordList.Name;
                        CategoryLabelContent = PasswordList.Category;
                    }

                    playerReady = false;
                    
                    if(GameIterations <= 0)
                    {
                        int result = Team2Score - Team1Score;
                        if (result > 0)
                        {
                            MessageBox.Show("Koniec gry!\n Wygrała drużyna: " + Team2Name);
                        }
                        else if (result == 0)
                        {
                            MessageBox.Show("Koniec gry!\n REMIS!");
                        }
                        else
                        {
                            MessageBox.Show("Koniec gry!\n Wygrała drużyna: " + Team1Name);
                        }

                        SetParametersDefault();
                        
                    }
                }

                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }

    }
}
