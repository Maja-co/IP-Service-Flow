using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;


namespace Business_Logic_Layer
{
    public class MaskineManager
    {
        // Repositories injiceres gennem constructor
        private readonly MaskineRepository _maskineRepo;
        private readonly KundeRepository _kundeRepo;

        public MaskineManager(MaskineRepository maskineRepo, KundeRepository kundeRepo)
        {
            _maskineRepo = maskineRepo;
            _kundeRepo = kundeRepo;
        }

        //US-2 & US-2.7: Forbinder en maskine til en kunde og gemmer ændringen i databasen.
        public void OpretMaskineForKunde(Maskine valgtMaskine, Kunde valgtKunde)
        {
            // Validering: Sikrer at hverken maskine eller kunde er tomme (null)
            if (valgtMaskine != null && valgtKunde != null)
            {
                // 1. Logik i hukommelsen: Opdaterer forbindelsen mellem objekterne
                valgtMaskine.SetKunde(valgtKunde);           
                valgtKunde.MaskineListe.Add(valgtMaskine);

                //US2.7- Gem ændringerne i databasen
                //Opdaterer maskinen i databasen med det nye KundeId
                _maskineRepo.Update(valgtMaskine);
            }

        }

        // US-2.11Grupperer kundens maskiner baseret på deres fabrikant (producent).
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
