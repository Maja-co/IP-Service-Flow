using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_Access_Layer.Services
{
    public class KundeService
    {
        private readonly MaskinContext _context;
        public KundeService(MaskinContext context)
        {
            _context = context;
        }

        public List<Kunde> GetAktiveKunder()
        {
            return _context.Kunder.Where(k => k.ErAktiv == true).ToList();
        }

        public void GemNyKunde(Kunde kunde)
        {
            _context.Kunder.Add(kunde);
            _context.SaveChanges();
        }

        public void OpdaterKunde(Kunde opdateretKunde)
        {
            _context.Kunder.Update(opdateretKunde);
            _context.SaveChanges();
        }

        public void DeaktiverKunde(int kundeId)
        {
            var kunde = _context.Kunder.Find(kundeId);
            if (kunde != null)
            {
                kunde.Deaktiver();
                _context.SaveChanges();
            }
        }
    }
}
