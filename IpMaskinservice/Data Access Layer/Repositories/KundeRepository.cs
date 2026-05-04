using Data_Access_Layer.Models;

namespace Data_Access_Layer.Repositories {
    // Staged
    public class KundeRepository {
        private readonly MaskinContext _db;

        public KundeRepository(MaskinContext db) {
            _db = db;
        }

        public void Add(Kunde kunde) {
            _db.Kunder.Add(kunde);
            _db.SaveChanges();
        }

        public List<Kunde> GetAll() {
            return _db.Kunder.ToList();
        }
    }
}