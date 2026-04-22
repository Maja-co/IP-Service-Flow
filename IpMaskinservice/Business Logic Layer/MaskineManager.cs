using Data_Access_Layer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer
{
    public class MaskineManager
    {
        //Samlet logik til at forbinde en maskine og en kunde korrekt
        public void OpretMaskineForKunde(Maskine valgtMaskine, Kunde valgtKunde)
        {
            if (valgtMaskine != null && valgtKunde != null)
            {
                // 1. Fortæl maskinen hvem dens ejer er
                valgtMaskine.SetKunde(valgtKunde);

                // 2. Tilføj maskinen til kundens oversigt (US-2 overblik)             
                valgtKunde.AddMaskineTilListe(valgtMaskine);
            }

        }

        // 2.metod at TestGruppering
        public Dictionary<string, List<Maskine>> GrupperMaskinerEfterProducent(Kunde kunde)
        {
            var dictionary = new Dictionary<string, List<Maskine>>();

            if (kunde == null || kunde.MaskineListe == null) return dictionary;

            foreach (var m in kunde.MaskineListe)
            {
                string producent = m.GetProducent() ?? "Ukendt";

                if (!dictionary.ContainsKey(producent))
                {
                    dictionary[producent] = new List<Maskine>();
                }
                dictionary[producent].Add(m);
            }

            return dictionary;
        }


    }
}
