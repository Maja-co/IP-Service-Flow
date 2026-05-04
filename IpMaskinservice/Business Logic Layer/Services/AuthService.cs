using System.Security.Cryptography;
using System.Text;
using Data_Access_Layer;
using Data_Access_Layer.Models;

namespace Business_Logic_Layer.Services {
    public class AuthService {
        private readonly IMedarbejderRepository _medarbejderRepo;

        public bool IsLoggedIn { get; private set; }
        public Medarbejder? CurrentUser { get; private set; }
        public string? CurrentSessionId { get; private set; }

        public AuthService(IMedarbejderRepository medarbejderRepo) {
            _medarbejderRepo = medarbejderRepo;
        }

        public async Task<(bool success, string sessionId)> LoginAsync(string email, string password) {
            var medarbejder = await _medarbejderRepo.GetByEmailAsync(email);

            if (medarbejder == null) return (false, string.Empty);

            // Kombiner kodeord med salt og beregn hash
            string beregnetHash = GenererSHA256Hash(password + medarbejder.Salt);

            if (beregnetHash != medarbejder.KodeOrdHash) {
                return (false, string.Empty);
            }

            // Opdater session i database
            string nySessionID = Guid.NewGuid().ToString();
            medarbejder.AktivSessionID = nySessionID;
            await _medarbejderRepo.UpdateAsync(medarbejder);

            // Gem session i hukommelsen
            IsLoggedIn = true;
            CurrentUser = medarbejder;
            CurrentSessionId = nySessionID;

            return (true, nySessionID);
        }

        private string GenererSHA256Hash(string input) {
            using (SHA256 sha256Hash = SHA256.Create()) {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public async Task<bool> IsSessionValid(int id, string sessionId) {
            var medarbejder = await _medarbejderRepo.GetByIdAsync(id);
            return medarbejder != null && medarbejder.AktivSessionID == sessionId;
        }

        public void Logout() {
            IsLoggedIn = false;
            CurrentUser = null;
            CurrentSessionId = null;
        }
    }
}