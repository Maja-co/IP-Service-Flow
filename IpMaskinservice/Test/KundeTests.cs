using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Data_Access_Layer;

namespace Test
{
    public class KundeTests
    {
        [Fact]
        public void OpretKunde_UdenFirmaNavn_KasterException()
        {
            // Arrange
            string ugyldigtNavn = ""; 

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Kunde(ugyldigtNavn, "Testvej 1", "Jens", "12345678", "jens@test.dk", true, 12345678)
            );
            Assert.Contains("Firmanavn", ex.Message);
        }

        [Fact]
        public void OpretKunde_UdenTelefonOgGyldigEmail_KasterException()
        {
            // Arrange: Telefon mangler, Email er der
            string telefon = "";
            string email = "test@test.dk";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Kunde("IP Maskinservice", "Testvej 1", "Mathias", telefon, email, true, 12345678)
            );
        }

        [Fact]
        public void OpretKunde_MedTelefonOgUdenEmail_KasterException()
        {
            // Arrange: Telefon er der, Email mangler
            string telefon = "88888888";
            string email = "";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Kunde("IP Maskinservice", "Testvej 1", "Mathias", telefon, email, true, 12345678)
            );
        }

        [Fact]
        public void OpretKunde_UdenTelefonOgEmail_KasterException()
        {
            // Arrange: telefon OG email mangler
            string telefon = "";
            string email = "";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Kunde("IP Maskinservice", "Testvej 1", "Mathias", telefon, email, true, 12345678)
            );
        }

        [Fact]
        public void Deaktiver_SaetterErAktivTilFalse_BevarerKunde()
        {
            // Arrange: Opret en gyldig, aktiv kunde
            var kunde = new Kunde("Firma A/S", "Vej 1", "Ulla", "88888888", "ulla@firma.dk", true, 12345678);

            // Act: Kald deaktiveringsmetoden
            kunde.Deaktiver();

            // Assert: Tjek at kunden nu er inaktiv (Soft Delete)
            Assert.False(kunde.ErAktiv);
        }

        [Fact]
        public void OpretKunde_UgyldigtCvrNummer_KasterException()
        {
            //Arrange
            int ugyldigtCvr = 12345; // CVR-numre skal være præcis 8 cifre, så dette er ugyldigt

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new Kunde("IP Maskinservice", "Testvej 1", "Mathias", "88888888", "mail@test.dk", true, ugyldigtCvr)
            );

            // Tjek at fejlbeskeden rent faktisk nævner CVR
            Assert.Contains("CVR", ex.Message);
        }
    }
}
