using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Repositories
{
    public class KundeRepository
    {
        private readonly MaskinContext _db;

        public KundeRepository(MaskinContext db)
        {
            _db = db;
        }

        public void Add(Kunde kunde)
        {
            _db.Kunder.Add(kunde); // Tilføj kunden til DbSet
            _db.SaveChanges();      // Gem denne kunde i databasen
        }

        public List<Kunde> GetAll() 
        { 
            return _db.Kunder.ToList();
        }
    }
}
