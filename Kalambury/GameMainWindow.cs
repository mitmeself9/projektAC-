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
using System.ComponentModel;
using System.Windows.Threading;

namespace Kalambury
{
    public static class ExtensionMethods
    {

        private static Action EmptyDelegate = delegate() { };


        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }

    public partial class MainWindow : Window , INotifyPropertyChanged
    {
        public void GameStart()
        {
            MessageBox.Show("gane");
        }
   

    public enum PasswordAmount { jedno = 1, trzy = 3, pięć = 5}

    public enum Time { jedna = 60 , dwie = 120 , trzy = 180}

    public enum FullRounds { jedna = 1, dwie = 2 , trzy = 3 }

    public enum TeamAmount { dwóch = 2, trzech = 3, czterech = 4, pięciu = 5, sześciu = 6 , siedmiu = 7 }


        private string teamLabelContent= "DRUŻYNA  >> 1  << ZGADUJE";
        public string TeamLabelContent
        {
            get { return this.teamLabelContent; }
            set
            {
                if (this.teamLabelContent != value)
                {
                    this.teamLabelContent = value;
                    OnPropertyChanged("TeamLabelContent");
                }
            }
        }

        private string playerLabelContent ="GRACZ 1 GOTOWY?";
        public string PlayerLabelContent
        {
            get { return this.playerLabelContent; }
            set
            {
                if (this.playerLabelContent != value)
                {
                    this.playerLabelContent = value;
                    OnPropertyChanged("PlayerLabelContent");
                }
            }
        }

        private string passwordLabelContent = String.Empty;
        public string PasswordLabelContent
        {
            get { return this.passwordLabelContent; }
            set
            {
                if (this.passwordLabelContent != value)
                {
                    this.passwordLabelContent = value;
                    OnPropertyChanged("PasswordLabelContent");
                }
            }
        }

        private string categoryLabelContent = String.Empty;
        public string CategoryLabelContent
        {
            get { return this.categoryLabelContent; }
            set
            {
                if (this.categoryLabelContent != value)
                {
                    this.categoryLabelContent = value;
                    OnPropertyChanged("CategoryLabelContent");
                }
            }
        }


        private string team1ScoreContent = "0 pkt";
        public string Team1ScoreContent
        {
            get { return this.team1ScoreContent; }
            set
            {
                if (this.team1ScoreContent != value)
                {
                    this.team1ScoreContent = value;
                    OnPropertyChanged("Team1ScoreContent");
                }
            }
        }

        private string team2ScoreContent = "0 pkt";
        public string Team2ScoreContent
        {
            get { return this.team2ScoreContent; }
            set
            {
                if (this.team2ScoreContent != value)
                {
                    this.team2ScoreContent = value;
                    OnPropertyChanged("Team2ScoreContent");
                }
            }
        }

        private int team1Score = 0;
        public int Team1Score
        {
            get { return this.team1Score; }
            set
            {
                if (this.team1Score != value)
                {
                    this.team1Score = value;
                    OnPropertyChanged("Team1Score");
                }
            }
        }

        private int team2Score = 0;
        public int Team2Score
        {
            get { return this.team2Score; }
            set
            {
                if (this.team2Score != value)
                {
                    this.team2Score = value;
                    OnPropertyChanged("Team2Score");
                }
            }
        }

        private string team1Name = String.Empty;
        public string Team1NameProp
        {
            get { return this.team1Name; }
            set
            {
                if (this.team1Name != value)
                {
                    this.team1Name = value;
                    OnPropertyChanged("Team1NameProp");
                }
            }
        }

        private string team2Name = String.Empty;
        public string Team2NameProp
        {
            get { return this.team2Name; }
            set
            {
                if (this.team2Name != value)
                {
                    this.team2Name = value;

                    OnPropertyChanged("Team2NameProp");
                }
            }
        }

        private int passwordAmountToQuess = 0 ;
        public int PasswordAmountToQuess
        {
            get { return this.passwordAmountToQuess; }
            set
            {
                if (this.passwordAmountToQuess != value)
                {
                    this.passwordAmountToQuess = value;
                }
            }
        }

        private int gameIterations = 0;
        public int GameIterations
        {
            get { return this.gameIterations; }
            set
            {
                if (this.gameIterations != value)
                {
                    this.gameIterations = value;
                }
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool playerReady = false;

        private bool teamToPLay = true;
        private bool team1 = false;
        private bool team2 = true;
        public bool TeamToPLay
        {
            get { return this.teamToPLay; }
            set
            {
                if (this.teamToPLay != value)
                {
                    this.teamToPLay = value;
                }
            }
        }

        private int timeChoosen = 0;
        public int TimeChoosen
        {
            get { return this.timeChoosen; }
            set
            {
                if (this.timeChoosen != value)
                {
                    this.timeChoosen = value;
                }
            }
        }
        
        private int timeForClock = 0;
        public int TimeForClock
        {
            get { return this.timeForClock; }
            set
            {
                if (this.timeForClock != value)
                {
                    this.timeForClock = value;
                    OnPropertyChanged("TimeForClock");
                }
            }
        }

        private DispatcherTimer Timer;


  }
    
}
