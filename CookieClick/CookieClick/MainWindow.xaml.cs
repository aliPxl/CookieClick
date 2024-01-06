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
using System.Windows.Threading;
using System.Numerics;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace CookieClick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //deze random word gebruikt om de kans te berekenen op de golden cookie
        Random random = new Random();

        // scorevar gaat omhoog bij elke klik of en omlaag bij elke investering 
        double scoreVar = 0;
        //totaalScore gaat alleen omhoog, dit is om de buttons die collapsed zijn visible te maken wanneer ze ooit x aantal koekjes hebben gehaald 
        double totaalScore = 0;
        //het inkomen dat de speler verdiend per seconde, deze gaat ook alleen omhoog per klik op een investering 
        double passieveInkomenVar = 0;
        // opbrengst is wat de speler krijgt per 10 milliseconde deze word uiteindelijk in de update functie aan scoreVar toegevoegd
        //
        double vermenigVuldiging = 1;
        //dit is bonus prijs verdubbeling na de eerste klik op bonus standaard 50 maar bij elke klik gaat deze maal 10 omhoogh  
        int bonusPrijsVerdubbeling = 50;
        //als de score kleiner is dan 1million maar groter dan duizend word de score terug gegeven met een spatie tussen de honderd en duizend tal 
        string ScoreString;
        //deze worden gebruikt in VertaalScore() 
        const double Duizend = 1000;
        const double million = 1000000;
        const double miljard = 1000000000;
        const double billion = 1000000000000;
        const double biljart = 1000000000000000;
        const double triljoen = 1000000000000000000;
        //1000000000000000000;
        //timer die ik oproep in mainwindow 
        DispatcherTimer timer;

//voor ele button hou ik een aantal variable bij 

         // de basis prijs 
         // het aantal keer dat deze investering is gekocht 
         // het aantal koekjes per seconde dat deze oplevert 

            //bonus 
          //totale opbrengst van de investering 
         // eerste prijs bonus button basisprijs maal 100
         // nieuwe prijs deze word berekend nadat er op een bonus is geklikt

        //                                               BUTTONS
        //cursor
        private const double cursorBasisPrijs = 15;
        double cursorAantalVar = 0;
        double cursorPerSeconde = 0.1;

       //bonus
        double cursorTotaalOpbrengst = 0;
        private const double cursorBonusBasisPrijs = cursorBasisPrijs*100;
        private  double cursorBonusNieuwPrijs = 0;



        //Grandma
        private const double grandmaBasisPrijs = 16;
        public int grandmaAantalVar = 0;
        private double grandmaPerSeconde = 1;

        //bonus
        double grandmaTotaalOpbrengst=0;
        private const double grandmaBonusBasisPrijs = grandmaBasisPrijs * 100;
        private double grandmaBonusNieuwPrijs = 0;

        //farm
        private const double farmBasisPrijs = 17;
        public int farmAantalVar = 0;
        private  double farmPerSeconde = 8;

        //bonus
        double farmTotaalOpbrengst = 0;
        private const double farmBonusBasisPrijs = farmBasisPrijs * 100;
        private double farmBonusNieuwPrijs = 0;

        //Mine
        private const double mineBasisPrijs = 18;
        public int mineAantalVar = 0;
        private  double minePerSeconde = 47;

        //bonus 
        double mineTotaalOpbrengst = 0;
        private const double mineBonusBasisPrijs = mineBasisPrijs * 100;
        private double mineBonusNieuwPrijs = 0;

        //Factory
        private const double factoryBasisPrijs = 19;
        public int factoryAantalVar = 0;
        private  double factoryPerSeconde = 260;

        //bonus
        double   factoryTotaalOpbrengst = 0;
        private const double factoryBonusBasisPrijs = factoryBasisPrijs * 100;
        private double factoryBonusNieuwPrijs = 0;

        //Bank
        private const double bankBasisPrijs = 20;
        public int bankAantalVar = 0;
        private  double bankPerSeconde = 1400;

        //bonus 
        double bankTotaalOpbrengst = 0;
        private const double bankBonusBasisPrijs = bankBasisPrijs * 100;
        private double bankBonusNieuwPrijs = 0;

        //Temple
        private const double templeBasisPrijs = 21;
        public int templeAantalVar = 0;
        private  double templePerSeconde = 7800;

        //bonus 
        double templeTotaalOpbrengst = 0;
        private const double templeBonusBasisPrijs = templeBasisPrijs * 100;
        private double templeBonusNieuwPrijs = 0;

/// <summary>
        /// <para>
        /// nieuwe timer word gestart op 10 ms</para>
        /// <para>(investeringen)pertienmiliseconde worden berekend altijd dinamic</para>
        /// <para>stackpanel zichtbare investeringen worden allemaal collapsed</para>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //timer word gestart die iedere 10 seconde iets doet 
            timer = new DispatcherTimer();
            timer.Tick += TickerUpdate;
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
            //Golden cookie timer per minuut 
            timer = new DispatcherTimer();
            timer.Tick += CalculateGoldenChances;
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Start();

            CursorPanel.Visibility = Visibility.Collapsed;
            GrandmaPanel.Visibility = Visibility.Collapsed;
            FarmPanel.Visibility = Visibility.Collapsed;
            MinePanel.Visibility = Visibility.Collapsed;
            FactoryPanel.Visibility = Visibility.Collapsed;
            BankPanel.Visibility = Visibility.Collapsed;
            TemplePanel.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// <para>kans = 0.3 (30%) er word een random aangemaakt tussen 0 en 1, </para>
        /// <para>als mijn kans groter is verschijnt golden cookie </para>
        /// </summary>
        private void CalculateGoldenChances(object sender, EventArgs e)
        {
            //nieuwe random tussen 0 en 1
            Random rnd = new Random();
            double random = rnd.NextDouble();

            //mijn kans (30%)
            double kans = 0.3;
            //als mijn kans groter is dan het random getal verschijnt de golden cookie op een plaats, gegenereerd door PlaceOfGoldenCookie()
            if (kans >= random)
            {
                PlaceOfGoldenCookie();
        }
        }
        private void PlaceOfGoldenCookie()
        {
            //twee randoms om de x en y possitie te bepalen 
            double randomX = random.Next(0, (int)(ActualWidth - GoldenCookie.Width));
            double randomY = random.Next(0, (int)(ActualHeight - GoldenCookie.Height));

            GoldenCookie.Margin = new Thickness(randomX, randomY, 0, 0);
            GoldenCookie.Visibility = Visibility.Visible;
        }
/// <summary>
        /// <para>dit is de mouse_down event voor het koekje</para>
        /// <para>koekje word even kleiner</para>
        /// <para>scorevar word met 1 verhoogd</para>
        /// <para>totaalScore word met 1 verhoogd</para>
        /// </summary>
        private void KoekjeImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //koekje word kleiner 
            KoekjeImg.Width = 130;
            //score word met 1 verhoogd
            scoreVar++;
            //totaalscore word met 1 verhoogd
            totaalScore++;
        }

/// <summary>
        /// zowel bij mouse_up als mouse_Leave krijgt het koekje de originele grote 
        /// </summary>
        private void KoekjeImg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KoekjeImg.Width = 150;
        }
        private void KoekjeImg_MouseLeave(object sender, MouseEventArgs e)
        {
            KoekjeImg.Width = 150;
        }

/// <summary>
        /// deze functie roept eigeinlijk om de 10 miliseconde de update functie op 
        /// </summary>
        private void TickerUpdate(object sender, EventArgs e)
        {
            UpdateFunctie();
        }

/// <summary>
        /// updateFunctie doet een berekening van ale scores  en een update van alle scores /content in de hele applicate
        /// <para>boven elke regel in deze functie  word er meer uitleg gegeven over wat elke regel precies doet </para>
        /// </summary>
        private void UpdateFunctie()
        {

            //investeringen worden pas zichtbaar als er genoeg koekjes zijn
            ShowButtons();
            //kan ik investeren worden buttons enabled or disabled
            CanInvest();
            //standaard zijn bonussen colapsed, indien genoeg koekjes worder deze wel zichtbaar 
            CanBuyFirstBonus();
            //eens de bonussen zichtbaar zijn maar er geen genoeg koekjes zijn om te kopen word de knop disabled 
            CanBuySecondBonus();


            //bij elke klik op een investering word de (opbrengst per 10 miliseconde)opgeslagen in de variable opbrengst
            //passiveincome is altijd per seconde vandaar /100 
            scoreVar+=passieveInkomenVar/100; //
            totaalScore+=passieveInkomenVar/100; //

            //als dat is berekend laat ik dit afgerond naar boven aan de gebruiker zien in mijn lblScore lable boven het koekje 
            LblScore.Content = VertaalScore(Math.Ceiling(Math.Round(scoreVar)));

            //ook word de score geupdate in de tietel van het scherm
            MW.Title = VertaalScore(Math.Ceiling(Math.Round(scoreVar)));

            //heir laat ik aan de gebruiker zien hoeeel hij passief (per seconde) verdient
            LblPassiveIncomen.Content = VertaalScore(passieveInkomenVar);

///hier word de inhoud van elke button geupdate 
            ///1. prijs word berekend door prijsBerekenen(basisprijs,aantal) deze functie geeft een return value en dat word mijn nieuwe prijs 
            ///2. aantal keer gekocht word ook geupdate 
            ///3. tooltip die laat zien hoeveel cookies per seconde deze investering oplevert

            //cursor       
            LblCursorPrijs.Content = VertaalScore(prijsBerekenen(cursorBasisPrijs, cursorAantalVar));
            LblCursorAantal.Content = cursorAantalVar;
            CursorBtn.ToolTip = $"opbrengst per seconde: {cursorPerSeconde}";

            //Grandma      
            LblGrandmaPrijs.Content = VertaalScore(prijsBerekenen(grandmaBasisPrijs, grandmaAantalVar));
            LblGrandmaAantal.Content = grandmaAantalVar;
            GrandmaBtn.ToolTip = $"opbrengst per seconde: {grandmaPerSeconde}";

            //farm          
            LblFarmPrijs.Content = VertaalScore(prijsBerekenen(farmBasisPrijs, farmAantalVar));
            LblFarmAantal.Content = farmAantalVar;
            FarmBtn.ToolTip = $"opbrengst per seconde: {farmPerSeconde}";

            //Mine          
            LblMinePrijs.Content = VertaalScore(prijsBerekenen(mineBasisPrijs, mineAantalVar));
            LblMineAantal.Content = mineAantalVar;
            MineBtn.ToolTip = $"opbrengst per seconde: {minePerSeconde}";

            //Factory  
            LblFactoryPrijs.Content = VertaalScore(prijsBerekenen(factoryBasisPrijs, factoryAantalVar));
            LblFactoryAantal.Content = factoryAantalVar;
            FactoryBtn.ToolTip = $"opbrengst per seconde: {factoryPerSeconde}";

            //Bank         
            LblBankPrijs.Content = VertaalScore(prijsBerekenen(bankBasisPrijs, bankAantalVar));
            LblBankAantal.Content = bankAantalVar;
            BankBtn.ToolTip = $"opbrengst per seconde: {bankPerSeconde}";

            //Temple        
            LblTemplePrijs.Content = VertaalScore(prijsBerekenen(templeBasisPrijs, templeAantalVar));
            LblTempleAantal.Content = templeAantalVar;
            TempleBtn.ToolTip = $"opbrengst per seconde: {templePerSeconde}";
        }

/// <summary>
        /// in deze functie vertaal ik het getal dat binnenkomt, 
        /// <para>als het getal boven de trillion ligt  komt het triljoental gevolgd door </para>
        /// <para>als het getal boven de biljart ligt  maar onder trillion komt het biljarttal gevolgd door 3 cijfers na de komma </para>
        /// <para>als het getal onder de million ligt maar boven de duizend krijgen een spatie tussen de honderd en duizendtallen</para>
        /// <para>alles onder  de duizend word gewoon gereturned als een string </para>
        /// </summary>
        /// <param name="score"> dit is het getal dat ik binnen krijg,(investering prijs,score,passiveincome)</param>
        /// <returns>de score in string vorm met eventueel million ect...</returns>
        private string VertaalScore(double score)
        {
            if (score >= triljoen)
            {
                double triljoenValue = Math.Round(score / triljoen, 3);
                return $"{triljoenValue:N3} triljoen";
            }

            if (score >= biljart && score<triljoen)
            {
                double biljartValue = Math.Round(score / biljart,3);
                return $"{biljartValue:N3} biljart";
            } 
            
            if (score >= billion && score<biljart)
            {
                double billionValue = Math.Round(score / billion,3);
                return $"{billionValue:N3} billion";
            }

            if (score >= miljard && score<billion)
            {
                double miljardValue = Math.Round(score / miljard,3);
                return $"{miljardValue:N3} miljard";
            }

            if (score >= million && score<miljard)
            {
                double millionValue = Math.Round(score / million,3);
                return $"{millionValue:N3} million";
            }

            if (score >= Duizend && score < million)
            {
                ScoreString = score.ToString().Insert(2, " ");
                return ScoreString;
            }

            return $"{score}";
        }
        
/// <summary>
        /// <para>in ShowButtons kijk ik of mijn totaalScore *(komt alleen maar bij gaat niks vanaf)* groter is dan basisPrijs van investeringen</para>
        /// <para> indien er in totaal genoeg koekjes zijn verzameld word de aankoop pas zichtbaar </para>
        /// </summary>
        private void ShowButtons()
        {
            //Cursor
            if (totaalScore >= cursorBasisPrijs || scoreVar >= cursorBasisPrijs)
            {
                CursorBtn.Visibility = Visibility.Visible;
            }
            //Grandma
            if (totaalScore >= grandmaBasisPrijs || scoreVar >= grandmaBasisPrijs)
            {
                GrandmaBtn.Visibility = Visibility.Visible;
            }
            //Farm
            if (totaalScore >= farmBasisPrijs || scoreVar >= farmBasisPrijs)
            {
                FarmBtn.Visibility = Visibility.Visible;
            }
            //Mine
            if (totaalScore >= mineBasisPrijs || scoreVar >= mineBasisPrijs)
            {
                MineBtn.Visibility = Visibility.Visible;
            }
            //Factory
            if (totaalScore >= factoryBasisPrijs || scoreVar >= factoryBasisPrijs)
            {
                FactoryBtn.Visibility = Visibility.Visible;
            }
            //Bank
            if (totaalScore >= bankBasisPrijs || scoreVar >= bankBasisPrijs)
            {
                BankBtn.Visibility = Visibility.Visible;
            }
            //Temple
            if (totaalScore >= templeBasisPrijs || scoreVar >= templeBasisPrijs)
            {
                TempleBtn.Visibility = Visibility.Visible;
            }
        }
        
/// <summary>
        /// in CanInvest ga ik kijken op basis van mijn score (gaat omhoog of omlaag)
        /// heb ik genoeg koekjes om te investeren dan word mijn button enabled, anders disabled 
        /// </summary>
        private void CanInvest()
        {
            //Cursor
            if (prijsBerekenen(cursorBasisPrijs, cursorAantalVar) > scoreVar)
            {
                CursorBtn.IsEnabled = false;
                LblCursorPrijs.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                CursorBtn.IsEnabled = true;
                LblCursorPrijs.Foreground = new SolidColorBrush(Colors.Green);
            }

            //Grandma
            if (prijsBerekenen(grandmaBasisPrijs, grandmaAantalVar) > scoreVar)
            {
                GrandmaBtn.IsEnabled = false;
                LblGrandmaPrijs.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                GrandmaBtn.IsEnabled = true;
                LblGrandmaPrijs.Foreground = new SolidColorBrush(Colors.Green);
            }

            //Farm
            if (prijsBerekenen(farmBasisPrijs, farmAantalVar) > scoreVar)
            {
                FarmBtn.IsEnabled = false;
                LblFarmPrijs.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FarmBtn.IsEnabled = true;
                LblFarmPrijs.Foreground = new SolidColorBrush(Colors.Green);
            }

            //Mine
            if (prijsBerekenen(mineBasisPrijs, mineAantalVar) > scoreVar)
            {
                MineBtn.IsEnabled = false;
                LblMinePrijs.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                MineBtn.IsEnabled = true;
                LblMinePrijs.Foreground = new SolidColorBrush(Colors.Green);
            }

            //Factory
            if (prijsBerekenen(factoryBasisPrijs, factoryAantalVar) > scoreVar)
            {
                FactoryBtn.IsEnabled = false;
                LblFactoryPrijs.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FactoryBtn.IsEnabled = true;
                LblFactoryPrijs.Foreground = new SolidColorBrush(Colors.Green);
            }

            //Bank
            if (prijsBerekenen(bankBasisPrijs, bankAantalVar) > scoreVar)
            {
                BankBtn.IsEnabled = false;
                LblBankPrijs.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                BankBtn.IsEnabled = true;
                LblBankPrijs.Foreground = new SolidColorBrush(Colors.Green);
            }

            //Temple
            if (prijsBerekenen(templeBasisPrijs, templeAantalVar) > scoreVar)
            {
                TempleBtn.IsEnabled = false;
                LblTemplePrijs.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                TempleBtn.IsEnabled = true;
                LblTemplePrijs.Foreground = new SolidColorBrush(Colors.Green);
            }
        }

/// <summary>
        /// <para>voor de eerste bonus word gekeken naar de basisprijs van elke bonus, deze is bovenaan al berekend basisprijs x100 </para>
        /// <para>bij genoeg koekjes word bonus zichtbaar, anders blijft de bonus button collapsed </para>
        /// </summary>
        private void CanBuyFirstBonus()
        {
            //cursor
            if (scoreVar >= cursorBonusBasisPrijs)
            {
                CursorBonus.Visibility = Visibility.Visible;
            }
            else if (scoreVar < cursorBonusBasisPrijs)
            {
                CursorBonus.Visibility=Visibility.Collapsed ;
            }
            //grandma
            if (scoreVar >= grandmaBonusBasisPrijs)
            {
                GrandmaBonus.Visibility = Visibility.Visible;
            }
            else 
            {
                GrandmaBonus.Visibility = Visibility.Collapsed;
            } 
            //farm
            if (scoreVar >= farmBonusBasisPrijs)
            {
                FarmBonus.Visibility=Visibility.Visible;
            }
            else 
            {
                FarmBonus.Visibility = Visibility.Collapsed;
            } 
            //mine
            if (scoreVar >= mineBonusBasisPrijs)
            {
                MineBonus.Visibility = Visibility.Visible;
            }
            else 
            {
                MineBonus.Visibility = Visibility.Collapsed;
            }
            //factory
            if (scoreVar >= factoryBonusBasisPrijs)
            {
                FactoryBonus.Visibility = Visibility.Visible;
            }
            else
            {
                FactoryBonus.Visibility = Visibility.Collapsed;
            }
            //bank
            if (scoreVar >= bankBonusBasisPrijs)
            {
                BankBonus.Visibility = Visibility.Visible;
            }
            else
            {
                BankBonus.Visibility = Visibility.Collapsed;
            }
            //temple
            if (scoreVar >= templeBonusBasisPrijs)
            {
                TempleBonus.Visibility = Visibility.Visible;
            }
            else
            {
                TempleBonus.Visibility = Visibility.Collapsed;
            }
        }

/// <summary>
        /// <para>omdat ook alle bonus buttons naar deze functie luisteren heb ik ook hier een switch gebruikt die gaat kijken welke button is geklikt de naam krijg ik van mijn sender</para>
        /// <para>bonusprijsverdubbeling staat bovenaan als 50  hier doe ik eerst maal 10, dan geef ik deze mee als parrameter met de functei die mijn nieuwe prijs gaat berekenen</para>
        /// <para>(inverstering) nieuwPrijs gelijkstellen aan de returnvalue van secondBonus(basisprijs,verdubbeling)</para>
        /// </summary>
        private void Bonus_Click(object sender, RoutedEventArgs e)
        {
            string BtnName = (sender as Button).Name;
            //for (cursoraantalBonus = 0; cursoraantalBonus < 5; vermenigVuldiging *= 2)
            switch (BtnName)
            {
                case "CursorBonus":
                    bonusPrijsVerdubbeling *= 10;
                    cursorBonusNieuwPrijs = SecondBonus(cursorBasisPrijs, bonusPrijsVerdubbeling);

                    vermenigVuldiging *= 2;
                    passieveInkomenVar -= cursorTotaalOpbrengst;
                    passieveInkomenVar += cursorTotaalOpbrengst * vermenigVuldiging;
                    cursorPerSeconde = cursorPerSeconde * vermenigVuldiging;
                    break;

                case "GrandmaBonus":
                    bonusPrijsVerdubbeling *= 10;
                    grandmaBonusNieuwPrijs = SecondBonus(grandmaBasisPrijs, bonusPrijsVerdubbeling);

                    vermenigVuldiging *= 2;
                    passieveInkomenVar -= grandmaTotaalOpbrengst;
                    passieveInkomenVar += grandmaTotaalOpbrengst * vermenigVuldiging;
                    grandmaPerSeconde *= vermenigVuldiging;
                    break;

                case "FarmBonus":
                    bonusPrijsVerdubbeling *= 10;
                    farmBonusNieuwPrijs = SecondBonus(farmBasisPrijs, bonusPrijsVerdubbeling);

                    vermenigVuldiging *= 2;
                    passieveInkomenVar -= farmTotaalOpbrengst;
                    passieveInkomenVar += farmTotaalOpbrengst*vermenigVuldiging;
                    farmPerSeconde *= vermenigVuldiging;
                    break;

                case "MineBonus":
                    bonusPrijsVerdubbeling *= 10;
                    mineBonusNieuwPrijs = SecondBonus(mineBasisPrijs, bonusPrijsVerdubbeling);
                    
                    vermenigVuldiging *= 2;
                    passieveInkomenVar -= mineTotaalOpbrengst;
                    passieveInkomenVar += mineTotaalOpbrengst*vermenigVuldiging;
                    minePerSeconde *= vermenigVuldiging;
                    break;

                case "FactoryBonus":
                    bonusPrijsVerdubbeling *= 10;
                    factoryBonusNieuwPrijs = SecondBonus(factoryBasisPrijs, bonusPrijsVerdubbeling);

                    vermenigVuldiging *= 2;
                    passieveInkomenVar -= factoryTotaalOpbrengst;
                    passieveInkomenVar += factoryTotaalOpbrengst *vermenigVuldiging;
                    factoryPerSeconde *= vermenigVuldiging;
                    break;

                case "BankBonus":
                    bonusPrijsVerdubbeling *= 10;
                    bankBonusNieuwPrijs = SecondBonus(bankBasisPrijs, bonusPrijsVerdubbeling);

                    vermenigVuldiging *= 2;
                    passieveInkomenVar -= bankTotaalOpbrengst;
                    passieveInkomenVar += bankTotaalOpbrengst * vermenigVuldiging;
                    bankPerSeconde *= vermenigVuldiging;
                    break;

                case "TempleBonus":
                    bonusPrijsVerdubbeling *= 10;
                    templeBonusNieuwPrijs = SecondBonus(templeBasisPrijs, bonusPrijsVerdubbeling);

                    vermenigVuldiging *= 2;
                    passieveInkomenVar -= templeTotaalOpbrengst;
                    passieveInkomenVar += templeTotaalOpbrengst * vermenigVuldiging;
                    templePerSeconde*= vermenigVuldiging;
                    break;

            }

        }

/// <summary>
        /// <para>hier word de prijs berekend voor elke bonus die minstens 1 keer is aangekocht</para>
        /// <para>bij elke klik op een bonus word de nieuwe prijs gelijkgesteld aan de return value van deze functie</para>
        /// </summary>
        /// <param name="basisPrijs">voor elke button is dit anders vb cursor =15 grandma 100</param>
        /// <param name="verdubbeling"> standaard 50 bij elke bonus klik word er maal 10 van gedaan en dan komt deze hier binnen dit kan zijn 500,5000 ect... </param>
        /// <returns>basisprijs maal verdubbeling</returns>
        private double SecondBonus(double basisPrijs,double verdubbeling)
        {
                return basisPrijs* verdubbeling;
        }

/// <summary>
        /// nadat de (investering)BonusNieuwprijs is berekend ga ik hier ook weer om de 10 milliseconde kijken of de button enabled or disabled moet worden
        /// </summary>
        private void CanBuySecondBonus()
        {
            //cursor
            if (scoreVar >= cursorBonusNieuwPrijs)
            {
                CursorBonus.IsEnabled = true;
            }
            else 
            {
                CursorBonus.IsEnabled = false;
            }
            //grandma
            if (scoreVar >= grandmaBonusNieuwPrijs)
            {
                GrandmaBonus.IsEnabled = true;
            }
            else
            {
                GrandmaBonus.IsEnabled = false;
            }
            //farm
            if (scoreVar >= farmBonusNieuwPrijs)
            {
                FarmBonus.IsEnabled = true;
            }
            else
            {
                FarmBonus.IsEnabled = false;
            }
            //mine
            if (scoreVar >= mineBonusNieuwPrijs)
            {
                MineBonus.IsEnabled = true;
            }
            else 
            {
                MineBonus.IsEnabled = false;
            }
            //factory
            if (scoreVar >= factoryBonusNieuwPrijs)
            {
                FactoryBonus.IsEnabled = true;
            }
            else 
            {
                FactoryBonus.IsEnabled = false;
            }
            //bank 
            if (scoreVar >= bankBonusNieuwPrijs)
            {
                BankBonus.IsEnabled = true;
            }
            else 
            {
                BankBonus.IsEnabled = false;
            }
            //temple 
            if (scoreVar >= templeBonusNieuwPrijs)
            {
                TempleBonus.IsEnabled = true;
            }
            else 
            {
                TempleBonus.IsEnabled = false;
            }
        }
    
/// <summary>
        /// elke investering button luisterd naar deze functie 
        /// daarom heb ik hier een switch case gebruikt op basis van de button name,
        /// in elke button gebeurd het volgende
        /// <para>1. de prijs van mijn investering word afgetrokken van scoreVar.</para>
        /// <para>2. van elke investering hou ik het aantal bij, deze gaat bij elke klik met 1 omhoog voor de betreffende investering.</para>
        /// <para>3. de (opbrengst per seconde) word opgeteld bij passiveincomevar deze variable is alleen om bij te houden wat de speler per seconde verdiend</para>
        /// <para>4. de (opbrengst per 10 milliseconde) word opgeteld bij (opbrengst) deze word in de update functie aan de score toegevoegd (per 10 miliseconde)</para>
        /// <para>5.  </para>
        /// <para>6. stackpanel van de investering word visible</para>
        /// <para>7. nieuwe image object word aangemaakt </para>
        /// <para>8. nieuwe image krijgt een source mee </para>
        /// <para>9. width, height en margin word toegevoegd aan de image</para>
        /// <para>10. image word toegevoegd aan de stackPanel.</para>
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ButtonName = (sender as Button).Name;
            switch (ButtonName)
            {
                //Cursor
                case "CursorBtn":
                    scoreVar -= prijsBerekenen(cursorBasisPrijs, cursorAantalVar);
                    cursorAantalVar++;
                    passieveInkomenVar += cursorPerSeconde;
                    //scoreVar += cursorPerTienMiliSeconde;//
                    cursorTotaalOpbrengst += cursorPerSeconde;
                    //
                    CursorPanel.Visibility = Visibility.Visible;
                            cursorTotaalOpbrengst = cursorPerSeconde * cursorAantalVar;
                            Image CursorImage = new Image();
                            CursorImage.Source = new BitmapImage(new Uri("/images/Cursor.png", UriKind.Relative));
                            CursorImage.Width = 100;
                            CursorImage.Height = 100;
                            CursorImage.Margin = new Thickness(5);
                            CursorPanel.Children.Add(CursorImage);
                    break;
                //Grandma
                case "GrandmaBtn":
                    scoreVar -= prijsBerekenen(grandmaBasisPrijs, grandmaAantalVar);
                    grandmaAantalVar++;
                    passieveInkomenVar += grandmaPerSeconde;
                   //scoreVar += grandmaPerTienMiliSeconde;//
                    grandmaTotaalOpbrengst += grandmaPerSeconde;
                    // 
                            GrandmaPanel.Visibility = Visibility.Visible;
                            Image GrandmaImage = new Image();
                            GrandmaImage.Source = new BitmapImage(new Uri("/images/supperGrandma.png", UriKind.Relative));
                            GrandmaImage.Width = 100;
                            GrandmaImage.Height = 100;
                            GrandmaImage.Margin = new Thickness(5);
                            GrandmaPanel.Children.Add(GrandmaImage);
                    break;
                //Farm
                case "FarmBtn":
                    scoreVar -= prijsBerekenen(farmBasisPrijs, farmAantalVar);
                    farmAantalVar++;
                    passieveInkomenVar += farmPerSeconde;
                    farmTotaalOpbrengst += farmPerSeconde;
                    //
                            FarmPanel.Visibility = Visibility.Visible;
                            Image FarmImage = new Image();
                            FarmImage.Source = new BitmapImage(new Uri("/images/Farm.png", UriKind.Relative));
                            FarmImage.Width = 100;
                            FarmImage.Height = 100;
                            FarmImage.Margin = new Thickness(5);
                            FarmPanel.Children.Add(FarmImage);
                    break;
                //Mine
                case "MineBtn":
                    scoreVar -= prijsBerekenen(mineBasisPrijs, mineAantalVar);
                    mineAantalVar++;
                    passieveInkomenVar += minePerSeconde;
                    mineTotaalOpbrengst += minePerSeconde;
                    //
                            MinePanel.Visibility = Visibility.Visible;
                            Image MineImage = new Image();
                            MineImage.Source = new BitmapImage(new Uri("/images/Mine.jpg", UriKind.Relative));
                            MineImage.Width = 100;
                            MineImage.Height = 100;
                            MineImage.Margin = new Thickness(5);
                            MinePanel.Children.Add(MineImage);
                    break;
                //Factory
                case "FactoryBtn":
                    scoreVar -= prijsBerekenen(factoryBasisPrijs,factoryAantalVar);
                    factoryAantalVar++;
                    passieveInkomenVar += factoryPerSeconde;
                    factoryTotaalOpbrengst += factoryPerSeconde;
                    //
                            FactoryPanel.Visibility = Visibility.Visible;
                            Image FactoryImage = new Image();
                            FactoryImage.Source = new BitmapImage(new Uri("/images/Factory.png", UriKind.Relative));
                            FactoryImage.Width = 100;
                            FactoryImage.Height = 100;
                            FactoryImage.Margin = new Thickness(5);
                            FactoryPanel.Children.Add(FactoryImage);
                    break;
                //Bank
                case "BankBtn":
                    scoreVar -= prijsBerekenen(bankBasisPrijs,bankAantalVar);
                    bankAantalVar++;
                    passieveInkomenVar += bankPerSeconde;
                    bankTotaalOpbrengst += bankPerSeconde;
                    //
                            BankPanel.Visibility = Visibility.Visible;
                            Image BankImage = new Image();
                            BankImage.Source = new BitmapImage(new Uri("/images/Bank.png", UriKind.Relative));
                            BankImage.Width = 100;
                            BankImage.Height = 100;
                            BankImage.Margin = new Thickness(5);
                            BankPanel.Children.Add(BankImage);
                    break;
                //Temple
                case "TempleBtn":
                    scoreVar -= prijsBerekenen(templeBasisPrijs,templeAantalVar);
                    templeAantalVar++;
                    passieveInkomenVar += templePerSeconde;
                    templeTotaalOpbrengst += templePerSeconde;
                    //
                            TemplePanel.Visibility = Visibility.Visible;
                            Image TempleImage = new Image();
                            TempleImage.Source = new BitmapImage(new Uri("/images/Temple.png", UriKind.Relative));
                            TempleImage.Width = 50;
                            TempleImage.Height = 50;
                            TempleImage.Margin = new Thickness(5);
                            TemplePanel.Children.Add(TempleImage);
                    break;
            }
        }
 
/// <summary>
        /// de formule van deze functie => standaardprijs maal (1.15 tot de macht van aantal) 
        /// </summary>
        /// <param name="standaardprijs">standaardprijs van mijn investering cursorprijs,grandmaprijs,farmprijs,minePrijs</param>
        /// <param name="aantal"> aantal keer gekocht </param>
        /// <returns> nieuwprijs voor elke investering deze word voor elke button geupdate in de update functie </returns>
        private double prijsBerekenen(double standaardprijs, double aantal)
        {
            double NieuwPrijs = Math.Ceiling(standaardprijs * Math.Pow(1.15, aantal));
            return NieuwPrijs;

        }
 
/// <summary>
        /// voor deze funcite heb ik gebruik gemaakt van de visualBasic interaction input box
        /// <para>eerst word de gebruiker gevraagd om een nieuwe naam in te geven voor zijn bakkery</para>
        /// <para>in een if else statment word vervolgens gecontrolleerd of de gebruiker iets heeft ingegeven </para>
        /// <para>als de gebruiker degelijk iet heeft ingetypt word de content van lable LblBakkerijNaam aangepast </para>
        /// <para>anders krijgt de gebruiker een melding dat de content niet leeg mag zijn of witruimte bevatten</para>
        /// <para>en word de standaard "PXL-BAKERY" text uiteraard niet vervangen </para>
        /// </summary>
        private void LblBakkerijNaam_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //content opvragen en opslaan als nieuweBakkerijNaam
            string nieuweBakkerijNaam = Interaction.InputBox("enter new name");

            if (!string.IsNullOrWhiteSpace(nieuweBakkerijNaam.Trim()))
            {
                // Update label content
                LblBakkerijNaam.Content = nieuweBakkerijNaam.Trim();
            }
            else
            {
                MessageBox.Show("content mag niet leeg zijn of leegruimte bevatten ");
            }

        }

/// <summary>
        /// <para>golden cookie is standaard disabled</para>
        /// <para>dit is de golden cookie als je hier op klikt word er 15 minuten aan passive invome bij scorevar  </para>
        /// <para>de berekening is eigenlijk heel simpel eerst doe ik passiveincome(dus per seconde) </para>
        /// <para>dat is het inkomen per minuut en dat maal 15 </para>
        /// <para>eens er op geklikt word word het koekje weer hidden</para>
        /// </summary
        private void GoldenCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passieveInkomenVar = (passieveInkomenVar * 60) * 15;
            GoldenCookie.Visibility = Visibility.Collapsed;
        }
        }
    }
}
