using System.Security.Cryptography;
using System.Text;
using Data_Access_Layer;
using Data_Access_Layer.Models;

namespace Business_Logic_Layer
{
    public class AuthService
    {
        private readonly IMedarbejderRepository _medarbejderRepo;

        // Egenskaber til at holde styr på sessionen i hukommelsen
        public bool IsLoggedIn { get; private set; }
        public Medarbejder? CurrentUser { get; private set; }
        public string? CurrentSessionId { get; private set; }

        public AuthService(IMedarbejderRepository medarbejderRepo)
        {
            _medarbejderRepo = medarbejderRepo;
        }

        public async Task<(bool success, string sessionId)> LoginAsync(string email, string password)
        {
            // Vi kalder variablerne noget helt andet lokalt for at undgå navne-forvirring
            string mailFraUI = email;
            string kodeFraUI = password;

            Console.WriteLine($"--- KONTROL-TJEK ---");
            Console.WriteLine($"Mail fra parameter: [{mailFraUI}]");
            Console.WriteLine($"Kode fra parameter: [{kodeFraUI}]");

            var medarbejder = await _medarbejderRepo.GetByEmailAsync(mailFraUI);

            if (medarbejder == null) return (false, string.Empty);

            // HER TVINGER VI DEN TIL AT BRUGE DE NYE NAVNE
            string denSamledeTekstDerSkalHashes = kodeFraUI + medarbejder.Salt;

            // --- DETTE ER DEN VIGTIGSTE LINJE ---
            Console.WriteLine($"DEN TEKST DER SENDES TIL HASH-FUNKTION: [{denSamledeTekstDerSkalHashes}]");

            string beregnetHash = GenererSHA256Hash(denSamledeTekstDerSkalHashes);

            Console.WriteLine($"Beregnet hash: {beregnetHash}");
            Console.WriteLine($"Database hash: {medarbejder.KodeOrdHash}");

            if (beregnetHash != medarbejder.KodeOrdHash)
            {
                return (false, string.Empty);
            }

            // SUCCESS LOGIK
            string nySessionID = Guid.NewGuid().ToString();
            medarbejder.AktivSessionID = nySessionID;
            await _medarbejderRepo.UpdateAsync(medarbejder);

            IsLoggedIn = true;
            CurrentUser = medarbejder;
            CurrentSessionId = nySessionID;

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

        public void Logout()
        {
            IsLoggedIn = false;
            CurrentUser = null;
            CurrentSessionId = null;
        }
    }
}
