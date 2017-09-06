using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Testujacy
{
    /// <summary>
    /// Kontener przechowujący wszyskie informacje o bazie
    /// </summary>
    public class BazaDanych
    {
        public UstawieniaBazy Ustawienia { get; set; }
        public List<Pytanie> Pytania { get; private set; }

        public BazaDanych()
        {
            Pytania = new List<Pytanie>();
        }
        public int SprawdzOdpowiedzi()
        {
            int wynik = 0;
            foreach(Pytanie pytanie in Pytania)
            {
                if (pytanie.SprawdzOdpowiedzi())
                    wynik++;
            }
            return wynik;
        }
    }
}
