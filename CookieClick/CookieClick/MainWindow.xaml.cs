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

namespace CookieClick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        int bonusPrijsVerdubbeling = 50;
        string ScoreString;
        const long million = 1000000;
        const long miljard = 1000000000;
        const long billion = 1000000000000;
        const long biljart = 1000000000000000;
        BigInteger triljoen = BigInteger.Parse("1000000000000000000");
            //1000000000000000000;

        const long honderdDuizend = 10000; 

        DispatcherTimer timer;
     ///   double ScoreVar = 1025643;
        double ScoreVar = 0;
        double TotaalScore = 0;
        //double ScoreVar = 0;
        int ButtonClicked = 0;
        double PasiveIncomenVar = 0;
        //                                               BUTTONS
        //cursor
        private const double BasisPrijsCursor = 15;
        double CursorAantalVar;
        private const double CursorPerSeconde=0.1;
        double cursorTotaalOpbrengst = 0;
        double cursorBonusAantal = 0;
        private const double cursorBonusBasisPrijs = cursorBasisPrijs*100;
        private  double cursorBonusNieuwPrijs = 0;

        //Granma
        private const double BasisPrijsGrandma = 16;
        public int GrandmaAantalVar;
        private const double GrandmaPerSeconde =1.0;

        double grandmaTotaalOpbrengst=0;
        double grandmaBonusAantal = 0;
        private const double grandmaBonusBasisPrijs = grandmaBasisPrijs * 100;
        private double grandmaBonusNieuwPrijs = 0;
        //farm
        private const double farmBasisPrijs = 17;
        public int farmAantalVar = 0;
        private const double farmPerSeconde = 8;
        double farmPerTienMiliSeconde = 0;
        //bonus
        double farmTotaalOpbrengst = 0;
        double farmBonusAantal = 0;
        private const double farmBonusBasisPrijs = farmBasisPrijs * 100;
        private double farmBonusNieuwPrijs = 0;

        //Mine
        private const double BasisPrijsMine = 18;
        public int MineAantalVar;
        private const double MinePerSeconde = 47.0;
        //bonus 
        double mineBonusAantal = 0;
        private const double mineBonusBasisPrijs = mineBasisPrijs * 100;
        private double mineBonusNieuwPrijs = 0;

        //Factory
        private const double BasisPrijsFactory = 19;
        public int FactoryAantalVar;
        private const double FactoryPerSeconde =260.0;
        double factoryBonusAantal = 0;
        private const double factoryBonusBasisPrijs = factoryBasisPrijs * 100;
        private double factoryBonusNieuwPrijs = 0;

        //Bank
        private const double BasisPrijsBank = 20;
        public int BankAantalVar;
        private const double BankPerSeconde =1400.0;
        double bankBonusAantal = 0;
        private const double bankBonusBasisPrijs = bankBasisPrijs * 100;
        private double bankBonusNieuwPrijs = 0;
        //Temple
        private const double BasisPrijsTemple = 21;
        public int TempleAantalVar;
        private const double TemplePerSeconde =7800.0;
        double templeBonusAantal = 0;
        private const double templeBonusBasisPrijs = templeBasisPrijs * 100;
        private double templeBonusNieuwPrijs = 0;

        /// <summary>
        /// nieuwe timer word gedeclareerd 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
           
            timer = new DispatcherTimer();
            timer.Tick += TickerUpdate;
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();

            timer = new DispatcherTimer();
            timer.Tick += PassiveIncome;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();

      
        }
        //TickerUpdate word om de 10 miliseconde gedaan
        private void TickerUpdate(object sender, EventArgs e)
        {
            // ScoreVar += alle verschillende passive inkomsten;

            //for elke soort inkomsten
            //    verhoog score op basis van hoeveelheid cursors/grandmas/... * inkomst van cursor/grandmas/...

            UpdateFunctie();
        }

        private void PassiveIncome(object sender, EventArgs e)
        {
            // ScoreVar += alle verschillende passive inkomsten;

            //for elke soort inkomsten
            //    verhoog score op basis van hoeveelheid cursors/grandmas/... * inkomst van cursor/grandmas/...
            ScoreVar += PasiveIncomenVar;
            LblPassiveIncomen.Content = $"+{PasiveIncomenVar}";
            UpdateFunctie();
        }

        //                                  koekjes_klick

        private void KoekjeImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //koekje word kleiner 
            KoekjeImg.Width = 130;
            //score word met 1 verhoogd
            ScoreVar++;
            TotaalScore++;
            CollapsButtons();
        }

        private void KoekjeImg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KoekjeImg.Width = 150;
        }

        private void KoekjeImg_MouseLeave(object sender, MouseEventArgs e)
        {
            KoekjeImg.Width = 150;
        }

        private void  TranslateScore(double number)
        {
            LblScore.Content = Math.Round(Math.Ceiling(ScoreVar), 1);
            //double Million = 1000000;
            //double HonderdDuizend = 10000;
             ScoreString = ScoreVar.ToString();
            //honderdduizend
            if (number >= honderdDuizend && number <= million)
            {
                ScoreString = ScoreString.Insert(2, " ");
                LblScore.Content= ScoreString;
            }
            //milion
            if (number >= million && number <= miljard)
            {
                ScoreString = ScoreString.Insert(2, " ");
                double  millionValue = Math.Round( number / million,3);
                LblScore.Content=$"{millionValue:N3} million";
            }
            //miljard
            if (number >= miljard && number <= billion)
            {
                double miljardValue = Math.Round(number / miljard, 2);
                LblScore.Content = $"{miljardValue:N3} Miljard";
                //    return $"{BillionValue} Billion ";
                //return $" { number / billion:N1} billion";
            }
            //billion
            if (number >= billion && number <= biljart)
            {
                double BillionValue = Math.Round(number / billion, 3);
                LblScore.Content = $"{BillionValue:N3} Billion";
                //    return $"{BillionValue} Billion ";
                //return $" { number / billion:N1} billion";
            }

        }
        private void UpdateFunctie()
        {
          //  CollapsButtons();
            CanInvest();
            TranslateScore(ScoreVar);
            //score word afgerond en getoond in de lable Boven 
            //LblScore.Content = Math.Round(ScoreVar, 1);
            //LblScore.Content = Math.Round( Math.Ceiling(ScoreVar),1);
       //     double afgekapt = Math.Round(double.Parse(TranslateScore(ScoreVar)));
           // MW.Title = TranslateScore(ScoreVar);
            // = TranslateScore(ScoreVar);
            LblPassiveIncomen.Content =$"+ {Math.Round(PasiveIncomenVar, 2)}";
           // MW.Title = Math.Round(double.Parse(TranslateScore(ScoreVar))).ToString();


            //nieuwe button !!!!!!!!!!!!!!
            //cursor        prijs in de button word eerst berekend en opgevuld, en aantal keren gekocht 
            LblCursorPrijs.Content = prijsBerekenen(BasisPrijsCursor, CursorAantalVar);
            LblCursorAantal.Content = CursorAantalVar;
            CursorBtn.ToolTip =$"opbrengst per seconde: {CursorPerSeconde}";
            //Grandma       prijs in de button word eerst berekend en opgevuld, en aantal keren gekocht 
            LblGrandmaPrijs.Content = prijsBerekenen(BasisPrijsGrandma, GrandmaAantalVar);
            LblGrandmaAantal.Content = GrandmaAantalVar;
            GrandmaBtn.ToolTip = $"opbrengst per seconde: {GrandmaPerSeconde}";  
            //farm          prijs in de button word eerst berekend en opgevuld, en aantal keren gekocht 
            LblFarmPrijs.Content = prijsBerekenen(BasisPrijsFarm, FarmAantalVar); ;
            LblFarmAantal.Content = FarmAantalVar;
            FarmBtn.ToolTip = $"opbrengst per seconde: {FarmPerSeconde}";
            //Mine          prijs in de button word eerst berekend en opgevuld, en aantal keren gekocht 
            LblMinePrijs.Content = prijsBerekenen(BasisPrijsMine, MineAantalVar); ;
            LblMineAantal.Content = MineAantalVar;
            MineBtn.ToolTip = $"opbrengst per seconde: {MinePerSeconde}"; ;
            //Factory          prijs in de button word eerst berekend en opgevuld, en aantal keren gekocht 
            LblFactoryPrijs.Content = prijsBerekenen(BasisPrijsFactory, FactoryAantalVar); ;
            LblFactoryAantal.Content = FactoryAantalVar;
            FactoryBtn.ToolTip = $"opbrengst per seconde: {FactoryPerSeconde}"; ;
            //Bank          prijs in de button word eerst berekend en opgevuld, en aantal keren gekocht 
            LblBankPrijs.Content = prijsBerekenen(BasisPrijsBank, BankAantalVar); ;
            LblBankAantal.Content = BankAantalVar;
            BankBtn.ToolTip= $"opbrengst per seconde: {BankPerSeconde}"; ;
            //Temple          prijs in de button word eerst berekend en opgevuld, en aantal keren gekocht 
            LblTemplePrijs.Content = prijsBerekenen(BasisPrijsTemple, TempleAantalVar); ;
            LblTempleAantal.Content = TempleAantalVar;
            TempleBtn.ToolTip = $"opbrengst per seconde: {TemplePerSeconde}"; ;
        }

        private void CollapsButtons()
        {
            //Cursor
            if (TotaalScore >= BasisPrijsCursor)
            {
                CursorBtn.Visibility = Visibility.Visible;
            }
            //Grandma
            if (TotaalScore >= BasisPrijsGrandma)
            {
                GrandmaBtn.Visibility = Visibility.Visible;
            }
            //Farm
            if (TotaalScore >= BasisPrijsFarm)
            {
                FarmBtn.Visibility = Visibility.Visible;
            }
            //Mine
            if (TotaalScore >= BasisPrijsMine)
            {
                MineBtn.Visibility = Visibility.Visible;
            }
            //Factory
            if (TotaalScore >= BasisPrijsFactory)
            {
                FactoryBtn.Visibility = Visibility.Visible;
            }
            //Bank
            if (TotaalScore >= BasisPrijsBank)
            {
                BankBtn.Visibility = Visibility.Visible;
            }
            //Temple
            if (TotaalScore >= BasisPrijsTemple)
            {
                TempleBtn.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// deze functie gaat kijken of ik genoeg koekjes heb om te investering, zodra deze genoeg is word button 
        /// enabled of indien niet genoeg word de button disabled, de basisprijs en het aantal keer gekocht word 
        /// megegeven als parameter om de nieuwe prijs te berekenen en returnt de nieuwprijs
            CanBuyFirstBonus();
            CanBuySecondBonus();
        /// </summary>
        private void CanInvest()
        {
           
            //Cursor
            if (prijsBerekenen(BasisPrijsCursor, CursorAantalVar) > ScoreVar)
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

            if (prijsBerekenen(BasisPrijsGrandma, GrandmaAantalVar) > ScoreVar)
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
            if (prijsBerekenen(BasisPrijsFarm, FarmAantalVar) > ScoreVar)
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
            if (prijsBerekenen(BasisPrijsMine, MineAantalVar) > ScoreVar)
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
            if (prijsBerekenen(BasisPrijsFactory, FactoryAantalVar) > ScoreVar)
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
            if (prijsBerekenen(BasisPrijsBank, BankAantalVar) > ScoreVar)
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
            if (prijsBerekenen(BasisPrijsTemple, TempleAantalVar) > ScoreVar)
            {
                TempleBtn.IsEnabled = false;
                LblTemplePrijs.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                TempleBtn.IsEnabled = true;
                LblTemplePrijs.Foreground = new SolidColorBrush(Colors.Green);
            }
            ShowButtons();
            CanBuyFirstBonus();
        }
        private void CanBuyFirstBonus()
        {
            //cursor
            if (scoreVar > cursorBonusBasisPrijs)
            {
                CursorBonus.Visibility = Visibility.Visible;
            }
            else if (scoreVar < cursorBonusBasisPrijs)
            {
                CursorBonus.Visibility=Visibility.Collapsed ;
            }
            //grandma
            if (scoreVar > grandmaBonusBasisPrijs)
            {
                GrandmaBonus.Visibility = Visibility.Visible;
            }
            else 
            {
                GrandmaBonus.Visibility = Visibility.Collapsed;
            } 
            //farm
            if (scoreVar > farmBonusBasisPrijs)
            {
                FarmBonus.Visibility=Visibility.Visible;
            }
            else 
            {
                FarmBonus.Visibility = Visibility.Collapsed;
            } 
            //mine
            if (scoreVar > mineBonusBasisPrijs)
            {
                MineBonus.Visibility = Visibility.Visible;
            }
            else 
            {
                MineBonus.Visibility = Visibility.Collapsed;
            }
            //factory
            if (scoreVar > factoryBonusBasisPrijs)
            {
                FactoryBonus.Visibility = Visibility.Visible;
            }
            else
            {
                FactoryBonus.Visibility = Visibility.Collapsed;
            }
            //bank
            if (scoreVar > bankBonusBasisPrijs)
            {
                BankBonus.Visibility = Visibility.Visible;
            }
            else
            {
                BankBonus.Visibility = Visibility.Collapsed;
            }
            //temple
            if (scoreVar > templeBonusBasisPrijs)
            {
                TempleBonus.Visibility = Visibility.Visible;
            }
            else
            {
                TempleBonus.Visibility = Visibility.Collapsed;
            }
        }
        private double SecondBonus(double basisPrijs,double verdubbeling)
        {
                return basisPrijs* verdubbeling;
        }

        private void CanBuySecondBonus()
        {
            //cursor
            if (scoreVar > cursorBonusNieuwPrijs)
            {
                CursorBonus.IsEnabled = true;
            }
            else if (scoreVar < cursorBonusNieuwPrijs)
            {
                CursorBonus.IsEnabled = false;
            }
            //grandma
            if (scoreVar > grandmaBonusNieuwPrijs)
            {
                GrandmaBonus.IsEnabled = true;
            }
            else if (scoreVar < grandmaBonusNieuwPrijs)
            {
                GrandmaBonus.IsEnabled = false;
            }
            //farm
            if (scoreVar > farmBonusNieuwPrijs)
            {
                FarmBonus.IsEnabled = true;
            }
            else if (scoreVar < farmBonusNieuwPrijs)
            {
                FarmBonus.IsEnabled = false;
            }
            //mine
            if (scoreVar > mineBonusNieuwPrijs)
            {
                MineBonus.IsEnabled = true;
            }
            else if (scoreVar < mineBonusNieuwPrijs)
            {
                MineBonus.IsEnabled = false;
            }
            //factory
            if (scoreVar > factoryBonusNieuwPrijs)
            {
                FactoryBonus.IsEnabled = true;
            }
            else if (scoreVar < factoryBonusNieuwPrijs)
            {
                FactoryBonus.IsEnabled = false;
            }
            //bank 
            if (scoreVar > bankBonusNieuwPrijs)
            {
                BankBonus.IsEnabled = true;
            }
            else if (scoreVar < bankBonusNieuwPrijs)
            {
                BankBonus.IsEnabled = false;
            }
            //temple 
            if (scoreVar > templeBonusNieuwPrijs)
            {
                TempleBonus.IsEnabled = true;
            }
            else if (scoreVar < templeBonusNieuwPrijs)
            {
                TempleBonus.IsEnabled = false;
            }
        }
    

        /// <summary>
        /// 
        /// </summary>
        private void Bonus_Click(object sender, RoutedEventArgs e)
        {
            string BtnName = (sender as Button).Name;
            //for (cursoraantalBonus = 0; cursoraantalBonus < 5; vermenigVuldiging *= 2)
            switch (BtnName)
            {
                case "CursorBonus":
                    cursorBonusAantal++;
                    bonusPrijsVerdubbeling *= 10;
                    //krijg ik de prijs terug na verdubbeling 
                    cursorBonusNieuwPrijs = SecondBonus(cursorBasisPrijs, bonusPrijsVerdubbeling);
                    //kijken of ik kan kopen of niet 
                  //  CanBuySecondBonus();
                    //passieveInkomenVar -= totaalOpbrengstCursor;
                    //passieveInkomenVar += totaalOpbrengstCursor * vermenigVuldiging;
                    //cursorPerSeconde = cursorPerSeconde * vermenigVuldiging;

                    break;

                case "GrandmaBonus":
                    grandmaBonusAantal++;
                    bonusPrijsVerdubbeling *= 10;
                    grandmaBonusNieuwPrijs = SecondBonus(grandmaBasisPrijs, bonusPrijsVerdubbeling);
                    //CanBuySecondBonus();
                    //passieveInkomenVar -= totaalOpbrengstGrandma;
                    //passieveInkomenVar += totaalOpbrengstGrandma * vermenigVuldiging;
                    //grandmaPerSeconde *= vermenigVuldiging;
                    break;

                case "FarmBonus":
                    farmBonusAantal++;
                    bonusPrijsVerdubbeling *= 10;
                    farmBonusNieuwPrijs = SecondBonus(farmBasisPrijs, bonusPrijsVerdubbeling);
                    //CanBuySecondBonus();
                    //bonus = scoreVar += (farmPerSeconde * farmAantalVar) * vermenigVuldiging;
                    //vermenigVuldiging *= 2;

                    break;

                case "MineBonus":
                    mineBonusAantal++;
                    bonusPrijsVerdubbeling *= 10;
                    mineBonusNieuwPrijs = SecondBonus(mineBasisPrijs, bonusPrijsVerdubbeling);
                    //CanBuySecondBonus();
                    //bonus = scoreVar += (minePerSeconde * mineAantalVar) * vermenigVuldiging;
                    //vermenigVuldiging *= 2;
                    break;

                case "FactoryBonus":
                    factoryBonusAantal++;
                    bonusPrijsVerdubbeling *= 10;
                    factoryBonusNieuwPrijs = SecondBonus(factoryBasisPrijs, bonusPrijsVerdubbeling);
                    //bonus = scoreVar += (factoryPerSeconde * factoryAantalVar) * vermenigVuldiging;
                    //vermenigVuldiging *= 2;
                    break;

                case "BankBonus":
                    bankBonusAantal++;
                    bonusPrijsVerdubbeling *= 10;
                    bankBonusNieuwPrijs = SecondBonus(bankBasisPrijs, bonusPrijsVerdubbeling);
                    //bonus = scoreVar += (bankPerSeconde * bankAantalVar) * vermenigVuldiging;
                    //vermenigVuldiging *= 2;
                    break;

                case "TempleBonus":
                    templeBonusAantal++;
                    bonusPrijsVerdubbeling *= 10;
                    templeBonusNieuwPrijs = SecondBonus(templeBasisPrijs, bonusPrijsVerdubbeling);
                    //bonus = scoreVar += (templePerSeconde * templeAantalVar) * vermenigVuldiging;
                    //vermenigVuldiging *= 2;
                    break;

        }

        // voor elke button geld het zelfde principe, dus elke button luistert naar een event 
        // aantal van die investering gaat omhoog,
        // score gaat omlaag met prijs van investering 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ButtonName = (sender as Button).Name;
            switch (ButtonName)
            {
                //Cursor
                case "CursorBtn":
                    CursorAantalVar++;
                    ScoreVar -= double.Parse(LblCursorPrijs.Content.ToString());
                    PasiveIncomenVar += CursorPerSeconde;
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
                    GrandmaAantalVar++;
                    ScoreVar -= double.Parse(LblGrandmaPrijs.Content.ToString());
                    PasiveIncomenVar += GrandmaPerSeconde;
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
                    FarmAantalVar++;
                    ScoreVar -= double.Parse(LblFarmPrijs.Content.ToString());
                    PasiveIncomenVar += FarmPerSeconde;
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
                    MineAantalVar++;
                    ScoreVar -= double.Parse(LblMinePrijs.Content.ToString());
                    PasiveIncomenVar += MinePerSeconde;
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
                    FactoryAantalVar++;
                    ScoreVar -= double.Parse(LblFactoryPrijs.Content.ToString());
                    PasiveIncomenVar += FactoryPerSeconde;
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
                    BankAantalVar++;
                    ScoreVar -= double.Parse(LblBankPrijs.Content.ToString());
                    PasiveIncomenVar += BankPerSeconde;
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
                    TempleAantalVar++;
                    ScoreVar -= double.Parse(LblTemplePrijs.Content.ToString());
                    PasiveIncomenVar += TemplePerSeconde;
                            TemplePanel.Visibility = Visibility.Visible;
                            Image TempleImage = new Image();
                            TempleImage.Source = new BitmapImage(new Uri("/images/Temple.png", UriKind.Relative));
                            TempleImage.Width = 50;
                            TempleImage.Height = 50;
                            TempleImage.Margin = new Thickness(5);
                            TemplePanel.Children.Add(TempleImage);
                    break;
            }
            TranslateScore(ScoreVar);
        }

        /// <summary>
        /// de formule van deze functie => standaardprijs maal (1.15 tot de macht van aantal) 
        /// </summary>
        /// <param name="standaardprijs">standaardprijs van mijn investering cursorprijs,grandmaprijs,farmprijs,minePrijs</param>
        /// <param name="aantal"> aantal keer gekocht </param>
        /// <returns> de berekende prijs </returns>
        private double prijsBerekenen(double standaardprijs, double aantal)
        {
            double NieuwPrijs = Math.Ceiling(standaardprijs * Math.Pow(1.15, aantal));
            return NieuwPrijs;

        }
        //mouseDown omdat het een lable is 
        private void LblBakkerijNaam_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //content opvragen en opslaan als nieuweBakkerijNaam
            string nieuweBakkerijNaam = Interaction.InputBox("enter new name");

            if (nieuweBakkerijNaam.Trim()   !="")
            {
                // Update label content
                LblBakkerijNaam.Content = nieuweBakkerijNaam;
            }
            else
            {
                MessageBox.Show("content mag niet leeg zijn of leegruimte bevatten ");
            }

        }

    }
}
