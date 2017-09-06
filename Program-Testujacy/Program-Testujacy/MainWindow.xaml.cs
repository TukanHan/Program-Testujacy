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

namespace Program_Testujacy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Główny węzeł aplikacji, z tego miejsca zmieniane są karty i stan programu
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum TrybyProgramu { Egzamin, Sprawdzie, WyborBazy };

        public static MainWindow mainWindowObject;
        public TrybyProgramu TrybProgramu { get; set; }

        private OknoEgzaminu oknoEgzaminu;

        public MainWindow()
        {
            mainWindowObject = this;
            InitializeComponent();
            TrybProgramu = TrybyProgramu.WyborBazy;
            this.Closing += (sender, e) => 
            {
                if(TrybProgramu == TrybyProgramu.Egzamin)
                {
                    MessageBoxResult dialog = MessageBox.Show("Jesteś w trakcie egzaminu, na pewno chcesz zamknąć program?", "Wyjście", MessageBoxButton.YesNo);
                    e.Cancel = dialog != MessageBoxResult.Yes;
                }
            };
        }

        public void WybranoBaze(string lokalizacjaBazyDanych)
        {
            BazaDanych bazaDanych = null;
            try
            {
                bazaDanych = Odczyt.WczytajBaze(lokalizacjaBazyDanych);               
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                Application.Current.Shutdown();
            }

            oknoWyborBazy.Visibility = Visibility.Hidden;

            Title = $"Program Testujący: {bazaDanych.Ustawienia.NazwaBazy}";
            TrybProgramu = TrybyProgramu.Egzamin;
            oknoEgzaminu = new OknoEgzaminu(bazaDanych);
            uchwytProgramu.Children.Add(oknoEgzaminu);
        }

        public void ZobaczWynik(int wynik, int liczbMozliwychPunktow)
        {
            TrybProgramu = TrybyProgramu.Sprawdzie;
            oknoEgzaminu.Visibility = Visibility.Hidden;
            oknoWyniku.Visibility = Visibility.Visible;
            oknoWyniku.UstawWynik(wynik, liczbMozliwychPunktow);
        }

        public void ZobaczOdpowiedzi()
        {
            oknoWyniku.Visibility = Visibility.Hidden;
            oknoEgzaminu.Visibility = Visibility.Visible;
            oknoEgzaminu.PrzejdzWTrybSprawdzania();
        }

        public void WybierzNowaBaze()
        {
            TrybProgramu = TrybyProgramu.WyborBazy;
            Title = $"Program Testujący";
            uchwytProgramu.Children.Remove(oknoEgzaminu);
            oknoWyniku.Visibility = Visibility.Hidden;
            oknoWyborBazy.Visibility = Visibility.Visible;
        }
    }
}
