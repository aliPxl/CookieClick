﻿using System;
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

namespace CookieClick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        double ScoreVar = 0;
        int ButtonClicked = 0;
        //double PasiveIncomenVar = 0;
        //                                               BUTTONS
        //cursor
        private const double BasisPrijsCursor = 15;
        double CursorAantalVar;
        private const double CursorPerSeconde=0.1;

        //Granma
        private const double BasisPrijsGrandma = 16;
        public int GrandmaAantalVar;
        private const double GrandmaPerSeconde =1.0;

        //farm
        private const double BasisPrijsFarm = 17;
        public int FarmAantalVar;
        private const double FarmPerSeconde = 8.0;
        //Mine
        private const double BasisPrijsMine = 18;
        public int MineAantalVar;
        private const double MinePerSeconde = 47.0;

        //Factory
        private const double BasisPrijsFactory = 19;
        public int FactoryAantalVar;
        private const double FactoryPerSeconde =260.0;

        //Bank
        private const double BasisPrijsBank = 20;
        public int BankAantalVar;
        private const double BankPerSeconde =1400.0;
        //Temple
        private const double BasisPrijsTemple = 21;
        public int TempleAantalVar;
        private const double TemplePerSeconde =7800.0;

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
        }
        //TickerUpdate word om de 10 miliseconde gedaan
        private void TickerUpdate(object sender, EventArgs e)
        {
            // ScoreVar += alle verschillende passive inkomsten;

            //for elke soort inkomsten
            //    verhoog score op basis van hoeveelheid cursors/grandmas/... * inkomst van cursor/grandmas/...

            UpdateFunctie();
        }

        //                                  koekjes_klick
        private void Koekje_Click(object sender, RoutedEventArgs e)
        {
            ScoreVar++;
        }

        private void KoekjeImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //koekje word kleiner 
            KoekjeImg.Width = 130;
            //score word met 1 verhoogd
            ScoreVar++;
        }

        private void KoekjeImg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KoekjeImg.Width = 150;
        }

        private void KoekjeImg_MouseLeave(object sender, MouseEventArgs e)
        {
            KoekjeImg.Width = 150;
        }
        private void UpdateFunctie()
        {
            CanInvest();

            //score word afgerond en getoond in de lable Boven 
            LblScore.Content = Math.Round(ScoreVar, 1);
            MW.Title = ScoreVar.ToString();

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

        /// <summary>
        /// deze functie gaat kijken of ik genoeg koekjes heb om te investering, zodra deze genoeg is word button 
        /// enabled of indien niet genoeg word de button disabled, de basisprijs en het aantal keer gekocht word 
        /// megegeven als parameter om de nieuwe prijs te berekenen en returnt de nieuwprijs
        /// </summary>
        private void CanInvest()
        {
            //Cursor
            if (prijsBerekenen(BasisPrijsCursor, CursorAantalVar) > ScoreVar)
            {
                CursorBtn.IsEnabled = false;
            }
            else
            {
                CursorBtn.IsEnabled = true;
            }
            //Grandma

            if (prijsBerekenen(BasisPrijsGrandma, GrandmaAantalVar) > ScoreVar)
            {
                GrandmaBtn.IsEnabled = false;
            }
            else
            {
                GrandmaBtn.IsEnabled = true;
            }
            //Farm
            if (prijsBerekenen(BasisPrijsFarm, FarmAantalVar) > ScoreVar)
            {
                FarmBtn.IsEnabled = false;
            }
            else
            {
                FarmBtn.IsEnabled = true;
            }
            //Mine
            if (prijsBerekenen(BasisPrijsMine, MineAantalVar) > ScoreVar)
            {
                MineBtn.IsEnabled = false;
            }
            else
            {
                MineBtn.IsEnabled = true;
            }

            //Factory
            if (prijsBerekenen(BasisPrijsFactory, FactoryAantalVar) > ScoreVar)
            {
                FactoryBtn.IsEnabled = false;
            }
            else
            {
                FactoryBtn.IsEnabled = true;
            }

            //Bank
            if (prijsBerekenen(BasisPrijsBank, BankAantalVar) > ScoreVar)
            {
                BankBtn.IsEnabled = false;
            }
            else
            {
                BankBtn.IsEnabled = true;
            }

            //Temple
            if (prijsBerekenen(BasisPrijsTemple, TempleAantalVar) > ScoreVar)
            {
                TempleBtn.IsEnabled = false;
            }
            else
            {
                TempleBtn.IsEnabled = true;
            }
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
                    break;
                //Grandma
                case "GrandmaBtn":
                    GrandmaAantalVar++;
                    ScoreVar -= double.Parse(LblGrandmaPrijs.Content.ToString());
                    break;
                //Farm
                case "FarmBtn":
                    FarmAantalVar++;
                    ScoreVar -= double.Parse(LblFarmPrijs.Content.ToString());
                    break;
                //Mine
                case "MineBtn":
                    MineAantalVar++;
                    ScoreVar -= double.Parse(LblMinePrijs.Content.ToString());
                    break;
                //Factory
                case "FactoryBtn":
                    FactoryAantalVar++;
                    ScoreVar -= double.Parse(LblFactoryPrijs.Content.ToString());
                    break;
                //Bank
                case "BankBtn":
                    BankAantalVar++;
                    ScoreVar -= double.Parse(LblBankPrijs.Content.ToString());
                    break;
                //Temple
                case "TempleBtn":
                    TempleAantalVar++;
                    ScoreVar -= double.Parse(LblTemplePrijs.Content.ToString());
                    break;
            }

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


    }
}
