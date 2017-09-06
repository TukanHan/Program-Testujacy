using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Testujacy
{
    public enum TrybPytania { Jednokrotne, Wielokrotne };

    public class Pytanie
    {
        public TrybPytania TrybPytania { get; private set; }
        public string TrescPytania { get; private set; }
        public List<Odpowiedz> Odpowiedzi { get; private set; }

        public Pytanie(string trescPytania, TrybPytania trybPytania)
        {
            TrybPytania = trybPytania;
            TrescPytania = trescPytania;
            Odpowiedzi = new List<Odpowiedz>();
        }

        public bool SprawdzOdpowiedzi()
        {
            foreach(Odpowiedz odpowiedz in Odpowiedzi)
            {
                if (!odpowiedz.SprawdzOdpowiedzi())
                    return false;
            }
            return true;
        }
    }
}
