using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Program_Testujacy
{
    /// <summary>
    /// Statyczna klasa odpowiedzialna za odczyt baz danych
    /// </summary>
    static class Odczyt
    {
        private static Encoding Kodowanie = Encoding.GetEncoding("Windows-1250");

        public static List<UstawieniaBazy> WczytajBazyZFolderu(string folder)
        {
            List<UstawieniaBazy> ustawieniaBaz = new List<UstawieniaBazy>();
            string[] plikiWFolderze = Directory.GetFiles(folder);
            foreach (string plikWFolderze in plikiWFolderze)
            {
                try
                {
                    if (Path.GetExtension(plikWFolderze).Equals(".baza"))
                    {
                        using (StreamReader sr = new StreamReader(plikWFolderze, Kodowanie))
                        {
                            ustawieniaBaz.Add(new UstawieniaBazy(sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), byte.Parse(sr.ReadLine()), byte.Parse(sr.ReadLine())) { Lokalizajca = plikWFolderze });
                        }
                    }
                }
                catch
                {
                    MessageBox.Show($"Struktura bazy {plikWFolderze} jest nieprawidłowa");
                }
            }
            return ustawieniaBaz;
        }

        public static BazaDanych WczytajBaze(string lokalizacja)
        {
            BazaDanych bazaDanych = null;
            using (StreamReader sr = new StreamReader(lokalizacja, Kodowanie))
            {
                bazaDanych = new BazaDanych();
                bazaDanych.Ustawienia = new UstawieniaBazy(sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), byte.Parse(sr.ReadLine()), byte.Parse(sr.ReadLine())) { Lokalizajca = lokalizacja };

                int liczbaPytan = int.Parse(sr.ReadLine());
                bool[] wylosowanePytania = LosujPytania(liczbaPytan, bazaDanych.Ustawienia.LiczbaPytanEgzaminacyjnych);

                for (int i=0; i<liczbaPytan; ++i)
                {
                    if(wylosowanePytania[i])
                    {
                        Pytanie pytanie = new Pytanie(sr.ReadLine(), (TrybPytania)Enum.Parse(typeof(TrybPytania), sr.ReadLine()));
                        bazaDanych.Pytania.Add(pytanie);

                        int liczbaOdpowiedzi = int.Parse(sr.ReadLine());
                        for (int j = 0; j < liczbaOdpowiedzi; ++j)
                        {
                            pytanie.Odpowiedzi.Add(new Odpowiedz(sr.ReadLine(), Boolean.Parse(sr.ReadLine())));
                        }
                    }                    
                }
            }
            return bazaDanych;
        }

        private static bool[] LosujPytania(int sumaPytan, int pytaniaDoLosowania)
        {
            Random generatorLosowosci = new Random();

            bool[] wylosowanePytania = new bool[sumaPytan];
            while (pytaniaDoLosowania-- != 0)
            {
                int wylosowanePole = generatorLosowosci.Next(0, sumaPytan);

                while (wylosowanePytania[wylosowanePole = (wylosowanePole + 1) % sumaPytan] == true) ;

                wylosowanePytania[wylosowanePole] = true;
            }

            return wylosowanePytania;
        }
    }
}
