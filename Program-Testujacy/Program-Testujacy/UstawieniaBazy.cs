using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Testujacy
{
    public class UstawieniaBazy
    {
        public string NazwaBazy { get; private set; }
        public string OpisBazy { get; private set; }
        public string AutorBazy { get; private set; }
        public byte CzasEgzaminu { get; private set; }
        public byte LiczbaPytanEgzaminacyjnych { get; private set; }
        public string Lokalizajca { get; set; }

        public UstawieniaBazy(string nazwaBazy, string opisBazy, string autorBazy, byte liczbaPytanEzaminacyjnych, byte czasEgzaminu)
        {
            NazwaBazy = nazwaBazy;
            OpisBazy = opisBazy;
            AutorBazy = autorBazy;
            CzasEgzaminu = czasEgzaminu;
            LiczbaPytanEgzaminacyjnych = liczbaPytanEzaminacyjnych;
        }
    }
}
