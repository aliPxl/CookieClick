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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookieClick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //score variable double
        double ScoreVar = 0;
        public MainWindow()
        {
            InitializeComponent();
            //score Update functie word opgeroepen deze zorgt ervoor dat de score aan de gebruiker word getoond
            ScoreUpdate();
        }
        //scoreUpdate functie
        private void ScoreUpdate()
        {
       //scorevar word nog altijd opgeslagen als commagetal maar word in de volgende relel naar onder afgerond
       //om aan de geburiker te laten zien
            ScoreVar=Math.Floor(ScoreVar);
            //scorelbl word opgevuld met de score 
            ScoreLbl.Content = $"score: {ScoreVar}";
        }
        //mouseDown event koekje
        private void KoekjeImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //koekje word kleiner 
            KoekjeImg.Width = 130;
            //score word met 1 verhoogd
            ScoreVar++;
            //score Update functie word opgeroepen deze zorgt ervoor dat de score aan de gebruiker word 
            ScoreUpdate();
        }
        //mouseUp event koekje
        private void KoekjeImg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //koekje original size
            KoekjeImg.Width = 150;
            //score Update functie word opgeroepen deze zorgt ervoor dat de score aan de gebruiker word getoond
            ScoreUpdate();
        }

        private void KoekjeImg_MouseLeave(object sender, MouseEventArgs e)
        {
            //koekje original size
            KoekjeImg.Width = 150;
            //score Update functie word opgeroepen deze zorgt ervoor dat de score aan de gebruiker word getoond
            ScoreUpdate();
        }
    }
}
