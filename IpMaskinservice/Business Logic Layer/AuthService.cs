using System.Security.Cryptography;
using System.Text;
using Data_Access_Layer;
using Data_Access_Layer.Models;

namespace Business_Logic_Layer
{
    public class AuthService
    {
        private readonly IMedarbejderRepository _medarbejderRepo;

        public AuthService(IMedarbejderRepository medarbejderRepo)
        {
            _medarbejderRepo = medarbejderRepo;
        }

        public async Task<(bool success, string sessionId)> LoginAsync(string email, string password)
        {
            // 1. Hent medarbejderen fra "databasen"
            var medarbejder = await _medarbejderRepo.GetByEmailAsync(email);

            if (medarbejder == null) return (false, string.Empty);

            // 2. Kombiner det indtastede password med medarbejderens unikke salt
            // Det er vigtigt at bruge præcis samme rækkefølge som da man oprettede brugeren
            string saltetPassword = password + medarbejder.Salt;

            // 3. Generer en hash af det indtastede password + salt
            string indtastetHash = GenererSHA256Hash(saltetPassword);

            // 4. Sammenlign med den hash, der ligger i databasen
            if (indtastetHash != medarbejder.KodeOrdHash)
            {
                return (false, string.Empty);
            }

            // 5. SUCCESS: Kør din Single Session logik
            string nySessionID = Guid.NewGuid().ToString();
            medarbejder.AktivSessionID = nySessionID;
            await _medarbejderRepo.UpdateAsync(medarbejder);

            return (true, nySessionID);
        }

        // Hjælpe-metode til at lave en sikker SHA256 hash
        private string GenererSHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Lav teksten om til bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Konverter bytes til en læsbar hex-streng (f.eks. "a1b2c3...")
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Metoden til at tjekke om sessionen stadig er gyldig
        public async Task<bool> IsSessionValid(int id, string sessionId)
        {
            var medarbejder = await _medarbejderRepo.GetByIdAsync(id);
            return medarbejder != null && medarbejder.AktivSessionID == sessionId;
        }
    }
}