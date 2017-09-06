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
    /// Interaction logic for OknoWyniku.xaml
    /// </summary>
    public partial class OknoWyniku : UserControl
    {
        public OknoWyniku()
        {
            InitializeComponent();
        }

        public void UstawWynik(int wynik, int liczbaPunktow)
        {
            punkty.Text = string.Concat(wynik.ToString(), "/", liczbaPunktow);
            werdykt.Text = (wynik >= (double)liczbaPunktow / 2) ? "Zaliczony" : "Niezaliczony";
        }

        private void przyciskSprawdz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindowObject.ZobaczOdpowiedzi();
        }

        private void przyciskNowyEgzamin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindowObject.WybierzNowaBaze();
        }
    }
}
