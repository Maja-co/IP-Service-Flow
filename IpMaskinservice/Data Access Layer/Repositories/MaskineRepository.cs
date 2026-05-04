using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Repositories
{
    public class MaskineRepository
    {
        private readonly MaskinContext _db;

        public MaskineRepository(MaskinContext db)
        {
            _db = db;
        }

        public void Add(Maskine maskine)
        {
            _db.Maskiner.Add(maskine); // Tilføj maskine til DbSet
            _db.SaveChanges();      // Gem denne maskine i databasen
        }

        public void Update(Maskine maskine)
        {
            _db.Maskiner.Update(maskine);
            _db.SaveChanges();
        }
    }
}
