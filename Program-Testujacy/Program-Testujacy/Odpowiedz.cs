using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_Testujacy
{
    public class Odpowiedz
    {
        public string TrescOdpowiedzi { get; private set; }
        public bool CzyPoprawna { get; private set; }
        public bool MojaOdpowiedz { get; set; }

        public Odpowiedz(string trescOdpowiedzi, bool czyPoprawna)
        {
            TrescOdpowiedzi = trescOdpowiedzi;
            CzyPoprawna = czyPoprawna;
        }

        public bool SprawdzOdpowiedzi()
        {
            return MojaOdpowiedz == CzyPoprawna;
        }
    }
}
