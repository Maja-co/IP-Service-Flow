using Moq;
using Xunit;
using Business_Logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using System.Security.Cryptography;
using System.Text;
using Business_Logic_Layer.Services;

namespace Test
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsSuccessAndNewSessionId()
        {
            // Arrange
            var mockRepo = new Mock<IMedarbejderRepository>();
            var authService = new AuthService(mockRepo.Object);

            string testPassword = "1234";
            string testSalt = "HemmeligtSalt123";

            string korrektHashIDatabase = GenererHashTilTest(testPassword + testSalt);

            var testBruger = new Medarbejder
            {
                Id = "1",
                MailAdresse = "test@maskin.dk",
                KodeOrdHash = korrektHashIDatabase,
                Salt = testSalt,
                AktivSessionID = null // Starter uden session
            };

            // Opsæt mock til at returnere vores testbruger
            mockRepo.Setup(r => r.GetByEmailAsync("test@maskin.dk"))
                    .ReturnsAsync(testBruger);

            // Act
            var (success, sessionId) = await authService.LoginAsync("test@maskin.dk", "1234");

            // Assert
            Assert.True(success);
            Assert.False(string.IsNullOrEmpty(sessionId)); // Der skal være genereret en ID

            // Verificer at repository blev bedt om at gemme ændringen (UpdateAsync)
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Medarbejder>(m => m.AktivSessionID == sessionId)), Times.Once());
        }

        [Fact]
        public async Task LoginAsync_WrongPassword_ReturnsFalseAndEmptySessionId()
        {
            // Arrange
            var mockRepo = new Mock<IMedarbejderRepository>();
            var authService = new AuthService(mockRepo.Object);
            var testBruger = new Medarbejder { MailAdresse = "test@maskin.dk", KodeOrdHash = "1234" };

            mockRepo.Setup(r => r.GetByEmailAsync("test@maskin.dk")).ReturnsAsync(testBruger);

            // Act
            var (success, sessionId) = await authService.LoginAsync("test@maskin.dk", "FORKERT_KODE");

            // Assert
            Assert.False(success);
            Assert.True(string.IsNullOrEmpty(sessionId));

            // UpdateAsync må IKKE være kaldt, da loginet fejlede
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Medarbejder>()), Times.Never());
        }

        [Fact]
        public async Task LoginAsync_UserNotFound_ReturnsFalse()
        {
            // Arrange
            var mockRepo = new Mock<IMedarbejderRepository>();
            var authService = new AuthService(mockRepo.Object);

            // Returner null for at simulere at brugeren ikke findes
            mockRepo.Setup(r => r.GetByEmailAsync("ikkeeksisterende@maskin.dk"))
                    .ReturnsAsync((Medarbejder)null);

            // Act
            var (success, sessionId) = await authService.LoginAsync("ikkeeksisterende@maskin.dk", "1234");

            // Assert
            Assert.False(success);
        }

        [Fact]
        public async Task IsSessionValid_CorrectSession_ReturnsTrue()
        {
            // Arrange
            var mockRepo = new Mock<IMedarbejderRepository>();
            var authService = new AuthService(mockRepo.Object);

            var aktivBruger = new Medarbejder
            {
                Id = "1",
                AktivSessionID = "GYLDIG_ID"
            };

            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(aktivBruger);

            // Act
            var isValid = await authService.IsSessionValid(1, "GYLDIG_ID");

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public async Task IsSessionValid_WrongSession_ReturnsFalse()
        {
            // Arrange
            var mockRepo = new Mock<IMedarbejderRepository>();
            var authService = new AuthService(mockRepo.Object);

            var aktivBruger = new Medarbejder
            {
                Id = "1",
                AktivSessionID = "NY_ID_FRA_ANDEN_BRUGER"
            };

            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(aktivBruger);

            // Act
            // Brugeren kommer med sin gamle ID, men databasen har "NY_ID_FRA_ANDEN_BRUGER"
            var isValid = await authService.IsSessionValid(1, "GAMMEL_ID_DER_ER_OVERSKREVET");

            // Assert
            Assert.False(isValid);
        }
        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var mockRepo = new Mock<IMedarbejderRepository>();
            var authService = new AuthService(mockRepo.Object);

            string testSalt = "NogleTilfældigeTegn123";
            string testPassword = "1234";
            // Vi simulerer hvordan hashen ville se ud i databasen:
            string korrektHashIDatabase = GenererHashTilTest(testPassword + testSalt);

            var testBruger = new Medarbejder
            {
                MailAdresse = "test@maskin.dk",
                Salt = testSalt,
                KodeOrdHash = korrektHashIDatabase
            };

            mockRepo.Setup(r => r.GetByEmailAsync("test@maskin.dk")).ReturnsAsync(testBruger);

            // Act
            var (success, _) = await authService.LoginAsync("test@maskin.dk", "1234");

            // Assert
            Assert.True(success);
        }

        // Tilføj denne metode i bunden af din test-klasse
        private string GenererHashTilTest(string input)
        {
            using (var sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                var builder = new System.Text.StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}