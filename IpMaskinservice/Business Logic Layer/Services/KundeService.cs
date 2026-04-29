using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business_Logic_Layer.Services
{
    public class KundeService
    {
        private readonly MaskinContext _context;
        public KundeService(MaskinContext context)
        {
            _context = context;
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
        public List<Kunde> GetAktiveKunder()
        {
            return _context.Kunder
                .Include(k => k.MaskineListe)
                .Where(k => k.ErAktiv == true)
                .ToList();
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
        public Kunde? GetKunde(int id)
        {
            return _context.Kunder.Find(id);
        }
        public List<Kunde> GetDeaktiveredeKunder()
        {
            return _context.Kunder
                .Include(k => k.MaskineListe)
                .Where(k => k.ErAktiv == false)
                .ToList();
        }

        public void AktiverKunde(int kundeId)
        {
            var kunde = _context.Kunder.Find(kundeId);
            if (kunde != null)
            {
                kunde.Aktiver();
                _context.SaveChanges();
            }
        }
    }
}
