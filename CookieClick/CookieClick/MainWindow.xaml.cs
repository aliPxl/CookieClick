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

        //Cursor
        //prijs Cursor
        double CursorPrijs = 15.00;
        //cursor per seconden
        double CursorSeconde=0.1;
        //count om bij te houden hoevaak de speler deze investering heeft gekocht hoeveel de speler hee
        int CursorAantal = 0;

        //Grandma
        // prijs Grandma
        double GrandmaPrijs = 2;// 100.00;
        //grandma per seconde
        double GrandmaSeconde = 1.0;
        //count om bij te houden hoevaak de speler deze investering heeft gekocht hoeveel de speler hee
        int GrandmaAantal = 0;

        //farm
        //prijs Farm
        double FarmPrijs = 3;//1100.00;
        //farm per seconde
        double FarmSeconde = 8.0;
        //count om bij te houden hoevaak de speler deze investering heeft gekocht hoeveel de speler hee
        int FarmAantal = 0;

        //mine
        //prijs Mine
        double MinePrijs = 4;//1200.00;
        //mine per seconde
        double MineSeconde = 47.0;
        //count om bij te houden hoevaak de speler deze investering heeft gekocht hoeveel de speler hee
        int MineAantal = 0;
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
            ///cursor groter dan de prijs dan word button enabled 
            if (ScoreVar >= CursorPrijs)
            {
                Cursor.IsEnabled = true;
                ScoreUpdate();
            }
            //zodra deze lager gaat word de button weer disabled 
            else if (ScoreVar < CursorPrijs)
            {
                Cursor.IsEnabled = false; 
                ScoreUpdate();
            }

            //Grandam groter dan de prijs dan word button enabled
            if (ScoreVar >= GrandmaPrijs)
            {
                Grandma.IsEnabled = true;
                ScoreUpdate();

            }
            //zodra deze lager gaat word de button weer disabled 
            else if (ScoreVar < GrandmaPrijs)
            {
                Grandma.IsEnabled = false;
                ScoreUpdate();
            }

            //Farm groter dan de prijs dan word button enabled
            if (ScoreVar >= FarmPrijs)
            {
                Farm.IsEnabled = true;
            }
            //zodra deze lager gaat word de button weer disabled 
            else if (ScoreVar <= FarmPrijs)
            {
                Farm.IsEnabled = false;
                ScoreUpdate();
            }

            //Mine groter dan de prijs dan word button enabled
            if (ScoreVar >= MinePrijs)
            {
                Mine.IsEnabled = true;
            }
            //zodra deze lager gaat word de button weer disabled 
            else if (ScoreVar <= MinePrijs)
            {
                Mine.IsEnabled = false;
                ScoreUpdate();
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

            /////////////////////////////////////////////////////////////////buttons 
            //Cursor
            LblCursorPrijs.Content = $"{CursorPrijs}";
            LblCursorAantal.Content = $"{CursorAantal}";
            Cursor.ToolTip= $"Aantal cookies per seconde: {CursorSeconde}";

            //Grandma
            LblGrandmaPrijs.Content = $"{GrandmaPrijs}";
            LblGrandmaAantal.Content = $"{GrandmaAantal}";
            Grandma.ToolTip = $"Aantal cookies per seconde: {GrandmaSeconde}";

            //Farm
              LblFarmPrijs.Content = $"{FarmPrijs}";
              LblFarmAantal.Content = $"{FarmAantal}";
              Farm.ToolTip = $"Aantal cookies per seconde: {FarmSeconde}";

            //Mine
            LblMinePrijs.Content = $"{MinePrijs}";
            LblMineAantal.Content = $"{MineAantal}";
            Mine.ToolTip = $"Aantal cookies per seconde: {MineSeconde}";
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

        private void Cursor_Click(object sender, RoutedEventArgs e)
        {
            CursorAantal++;
            ScoreVar -= CursorPrijs;
            ScoreUpdate();
            EnableOrDisable();
        }

        private void Grandma_Click(object sender, RoutedEventArgs e)
        {
            GrandmaAantal++;
            ScoreVar -= GrandmaPrijs;
            ScoreUpdate();
            EnableOrDisable();
        }

        private void Farm_Click(object sender, RoutedEventArgs e)
        {
            FarmAantal++;
            ScoreVar -= FarmPrijs;
            ScoreUpdate();
            EnableOrDisable();
        }

        private void Mine_Click(object sender, RoutedEventArgs e)
        {
            MineAantal++;
            ScoreVar -= MinePrijs;
            ScoreUpdate();
            EnableOrDisable();
        }
    }
}
