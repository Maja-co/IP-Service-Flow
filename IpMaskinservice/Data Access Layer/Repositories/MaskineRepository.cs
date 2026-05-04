using Data_Access_Layer.Models;

namespace Data_Access_Layer.Repositories {
    // Staged
    public class MaskineRepository {
        private readonly MaskinContext _db;

        public MaskineRepository(MaskinContext db) {
            _db = db;
        }

        public void Add(Maskine maskine) {
            _db.Maskiner.Add(maskine);
            _db.SaveChanges();
        }

        public void Update(Maskine maskine) {
            _db.Maskiner.Update(maskine);
            _db.SaveChanges();
        }
    }
}