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

namespace Program_Testujacy
{
    /// <summary>
    /// Interaction logic for OknoEgzaminu.xaml
    /// </summary>
    public partial class OknoEgzaminu : UserControl
    {
        private BazaDanych bazaDanych;
        private int indexPytania;   

        private DispatcherTimer dispatcherTimer;
        private EventHandler zegar;
        private int timer;

        public OknoEgzaminu(BazaDanych bazaDanych)
        {
            InitializeComponent();
            this.bazaDanych = bazaDanych;

            zegar = (object sender, EventArgs e) =>
            {
                timer--;
                czas.Text = string.Format("{0:00}:{1:00}", (timer / 60), (timer % 60));
                if (timer == 0)
                    ZakonczEgzamin();
            };

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += zegar;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            timer = bazaDanych.Ustawienia.CzasEgzaminu*60 > 3600 ? 3600 : bazaDanych.Ustawienia.CzasEgzaminu * 60;
            czas.Text = string.Format("{0:00}:{1:00}", (timer / 60), (timer % 60));
            dispatcherTimer.Start();

            OtworzPytanie(0);
        }

        public void PrzejdzWTrybSprawdzania()
        {
            OtworzPytanie(0);
        }

        private void OtworzPytanie(int index)
        {
            indexPytania = index;

            trescPytania.Text = bazaDanych.Pytania[index].TrescPytania;
            przyciskPoprzedni.IsEnabled = index != 0;
            przyciskNastepny.IsEnabled = index < bazaDanych.Pytania.Count - 1;
            strony.Text = $"{index + 1} na {bazaDanych.Pytania.Count}";

            OdswierzOdpowiedzi(bazaDanych.Pytania[index]);  
        }

        private void przyciskPoprzedni_Click(object sender, RoutedEventArgs e)
        {
            OtworzPytanie(indexPytania - 1);
        }

        private void przyciskNastepny_Click(object sender, RoutedEventArgs e)
        {
            OtworzPytanie(indexPytania + 1);
        }

        private void przyciskZakoncz_Click(object sender, RoutedEventArgs e)
        {
            ZakonczEgzamin();
        }

        private void OdswierzOdpowiedzi(Pytanie pytanie)
        {
            var obiektyDoUsuniecia = uchwyt.Children.OfType<CheckBox>().ToArray();
            foreach(var obiektDoUsuniecia in obiektyDoUsuniecia)
            {
                uchwyt.Children.Remove(obiektDoUsuniecia);
            }

            foreach(Odpowiedz odpowiedz in pytanie.Odpowiedzi)
            {
                CheckBox checkbox = new CheckBox()
                {
                    Width = 650,
                    IsChecked = odpowiedz.MojaOdpowiedz,
                    Margin = new Thickness(Margin.Left, Margin.Top + 5, Margin.Right, Margin.Bottom),
                    IsEnabled = MainWindow.mainWindowObject.TrybProgramu == MainWindow.TrybyProgramu.Egzamin ? true : false,
                    Content = new TextBlock()
                    {
                        Text = odpowiedz.TrescOdpowiedzi,
                        FontSize = 16,
                        TextWrapping = TextWrapping.Wrap,
                        FontFamily = new FontFamily("Microsoft JhengHei UI Light"),
                        Foreground = MainWindow.mainWindowObject.TrybProgramu == MainWindow.TrybyProgramu.Sprawdzie ? (odpowiedz.CzyPoprawna ? Brushes.Green : Brushes.Red) : Brushes.Black
                    }
                };

                uchwyt.Children.Add(checkbox);

                if (pytanie.TrybPytania == TrybPytania.Jednokrotne)
                {
                    checkbox.Checked += (s, args) =>
                    {
                        for (int i = 1; i < uchwyt.Children.Count; ++i)
                        {
                            CheckBox innyCheckBox = uchwyt.Children[i] as CheckBox;
                            if (innyCheckBox == s)
                                pytanie.Odpowiedzi[i-1].MojaOdpowiedz = true;
                            else
                                innyCheckBox.IsChecked = pytanie.Odpowiedzi[i-1].MojaOdpowiedz = false;
                        }
                    };
                    checkbox.Unchecked += (s, args) =>
                    {
                        for (int i = 1; i < uchwyt.Children.Count; ++i)
                        {
                            if (uchwyt.Children[i-1] == s)
                            {
                                pytanie.Odpowiedzi[i-1].MojaOdpowiedz = false;
                                break;
                            }
                        }
                    };
                }
                else
                {
                    Binding bindingPoprawnaOdpowiedz = new Binding("MojaOdpowiedz") { Source = odpowiedz, Mode = BindingMode.TwoWay };
                    checkbox.SetBinding(CheckBox.IsCheckedProperty, bindingPoprawnaOdpowiedz);
                }   
            }
        }

        private void ZakonczEgzamin()
        {
            dispatcherTimer.Tick -= zegar;
            czas.Visibility = Visibility.Hidden;

            MainWindow.mainWindowObject.ZobaczWynik(bazaDanych.SprawdzOdpowiedzi(),bazaDanych.Pytania.Count);
        }      
    }
}
