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
        //prijs Cursor
        double CursorPrijs = 1;//15.00;
        // prijs Grandma
        double GrandmaPrijs = 2;// 100.00;
        //prijs Farm
        double FarmPrijs = 3;//1100.00;
        //prijs Mine
        double MinePrijs = 4;//1200.00;
        public MainWindow()
        {
            InitializeComponent();
            //score Update functie word opgeroepen deze zorgt ervoor dat de score aan de gebruiker word getoond
            ScoreUpdate();
        }
        //eigen functies 
        //functie om te checken welke button enabled kan worden
        private void EnableOrDisable()
        {
            if (ScoreVar >= CursorPrijs)
            {
                Cursor.IsEnabled = true;
            }
             if (ScoreVar >= GrandmaPrijs)
            {
                Grandma.IsEnabled = true;
            }
             if (ScoreVar >= FarmPrijs)
            {
                Farm.IsEnabled = true;
            }
             if (ScoreVar >= MinePrijs)
            {
                Mine.IsEnabled = true;
            }

        }
        //scoreUpdate functie
        private void ScoreUpdate()
        {
       //scorevar word nog altijd opgeslagen als commagetal maar word in de volgende relel naar onder afgerond
       //om aan de geburiker te laten zien
            ScoreVar=Math.Floor(ScoreVar);
            //scorelbl word opgevuld met de score 
            ScoreLbl.Content = $"score: {ScoreVar}";
            //tietel word ook geUpdate MW is de name van mij MainWindow
            MW.Title = $"{ScoreVar}";
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
            EnableOrDisable();
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
