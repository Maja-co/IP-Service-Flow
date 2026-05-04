using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Business_Logic_Layer.Services {
    public class MedarbejderService {
        private readonly MaskinContext _context;

        public MedarbejderService(MaskinContext context) {
            _context = context;
        }

        public async Task<List<Medarbejder>> HentAlleMedarbejdereMedAftalerAsync() {
            return await _context.Medarbejdere
                .Include(m => m.ServiceOpgaveListe)
                .ThenInclude(s => s.Maskine)
                .ThenInclude(m => m.Kunde)
                .ToListAsync();
        }

        public async Task SletMedarbejderAsync(string id) {
            var medarbejder = await _context.Medarbejdere.FindAsync(id);
            if (medarbejder != null) {
                _context.Medarbejdere.Remove(medarbejder);
                await _context.SaveChangesAsync();
            }
        }

        public async Task FlytAnsvarAsync(string fraMedarbejderId, string tilMedarbejderId, List<int> aftaleIds) {
            var aftaler = await _context.ServiceOpgaver
                .Where(o => aftaleIds.Contains(o.Id) && o.MedarbejderId == fraMedarbejderId)
                .ToListAsync();

            foreach (var aftale in aftaler) {
                aftale.MedarbejderId = tilMedarbejderId;
            }

            await _context.SaveChangesAsync();
        }

        public async Task GemNyMedarbejderAsync(Medarbejder medarbejder) {
            if (medarbejder == null)
                throw new ArgumentNullException(nameof(medarbejder), "Medarbejder kan ikke være null");

            if (string.IsNullOrWhiteSpace(medarbejder.Id))
                throw new ArgumentException("Medarbejder ID kan ikke være tom", nameof(medarbejder));

            if (string.IsNullOrWhiteSpace(medarbejder.MedarbejderNavn))
                throw new ArgumentException("Medarbejdernavn kan ikke være tomt", nameof(medarbejder));

            var eksisterende = await _context.Medarbejdere.FindAsync(medarbejder.Id);
            if (eksisterende != null)
                throw new ArgumentException("En medarbejder med dette ID eksisterer allerede", nameof(medarbejder));

            _context.Medarbejdere.Add(medarbejder);
            await _context.SaveChangesAsync();
        }

        // Hjælpemetode til at generere salt og hash
        public static (string salt, string hash) GenererSaltOgHash(string kodeord) {
            if (string.IsNullOrWhiteSpace(kodeord))
                throw new ArgumentException("Kodeord kan ikke være tomt", nameof(kodeord));

            // Generer salt
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(saltBytes);
            }

            string salt = Convert.ToBase64String(saltBytes);

            // Generer hash med salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(kodeord, saltBytes, 10000, HashAlgorithmName.SHA256)) {
                byte[] hash = pbkdf2.GetBytes(32);
                string hashString = Convert.ToBase64String(hash);
                return (salt, hashString);
            }
        }
    }
}