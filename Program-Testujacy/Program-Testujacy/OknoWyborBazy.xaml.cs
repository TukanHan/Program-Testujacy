using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Program_Testujacy
{
    /// <summary>
    /// Interaction logic for OknoWyborBazy.xaml
    /// Miejsce startowe, wczytuje i wyświetla dostępne dla programu bazy
    /// </summary>
    public partial class OknoWyborBazy : UserControl
    {
        public OknoWyborBazy()
        {
            InitializeComponent();

            List<UstawieniaBazy> ustawieniaBaz = null;
            try
            {
                ustawieniaBaz = Odczyt.WczytajBazyZFolderu("Bazy");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                Application.Current.Shutdown(1);
            }

            string szablonReprezentacjiBazy = XamlWriter.Save(SzablonReprezentacjiBazy);
            uchwyt.Children.Remove(SzablonReprezentacjiBazy);

            foreach(UstawieniaBazy ustawienia in ustawieniaBaz)
            {
                StringReader stringReader = new StringReader(szablonReprezentacjiBazy);
                XmlReader xmlReader = XmlReader.Create(stringReader);
                Grid nowaReprezentacja = (Grid)XamlReader.Load(xmlReader);
                uchwyt.Children.Add(nowaReprezentacja);

                (nowaReprezentacja.FindName("nazwaBazy") as TextBlock).Text = ustawienia.NazwaBazy;
                (nowaReprezentacja.FindName("opisBazy") as TextBlock).Text = ustawienia.OpisBazy;
                (nowaReprezentacja.FindName("autorBazy") as TextBlock).Text = ustawienia.AutorBazy == String.Empty ? "Anonim" : ustawienia.AutorBazy;
                (nowaReprezentacja.FindName("czasEgzaminu") as TextBlock).Text = $"Czas: {ustawienia.CzasEgzaminu} min";
                (nowaReprezentacja.FindName("liczbaPytan") as TextBlock).Text = $"Liczba pytań: {ustawienia.LiczbaPytanEgzaminacyjnych}";
                (nowaReprezentacja.FindName("przyciskWybierz") as Button).Click += (sender, e) =>
                {
                    MainWindow.mainWindowObject.WybranoBaze(ustawienia.Lokalizajca);
                };
            }
        }

        private void przyciskWyjscia_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void przyciskOProgramie_Click(object sender, RoutedEventArgs e)
        {
            new OknoOProgramie().Show();       
        }
    }
}
