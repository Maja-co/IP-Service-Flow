using Data_Access_Layer;
using Business_Logic_Layer;
using Xunit;

namespace Test;

public class UnitTest1 {
    [Fact]
    public void OpretMaskineForKundeTest() {

        // Arrange
        var manager = new MaskineManager();

        //opret en testkunde og opret en testmaskine
        var testKunde = new Kunde("Test Firma", "Gade 1 ", "Person", "12345678", "mail@test.dk", true, 12345678);
        var testMaskine = new Maskine("SN-999", "TestBrand", testKunde,MaskineType.Savning);

        // Act
        manager.OpretMaskineForKunde(testMaskine, testKunde);

        // Assert
        Assert.NotNull(testKunde);
        Assert.NotNull(testMaskine);

        // Tjek om forbindelsen er oprettet korrekt i listen
        Assert.Contains(testMaskine, testKunde.MaskineListe);

    }
    [Fact]
    public void TestGruppering() {

        //Arrange
        var manager = new MaskineManager();
        var Kunde = new Kunde("Test Firma 1", "Gade 1 ", "Person 1", "12345678", "mail@test.dk", true, 12345678);

        //Act
        var maskine1 = new Maskine("SN-001", "Volvo", Kunde, MaskineType.Savning);
        var maskine2 = new Maskine("SN-002", "Volvo", Kunde, MaskineType.Boring);
        var maskine3 = new Maskine("SN-003", "Scania", Kunde, MaskineType.Rørbukning);

        //Assert
        Kunde.AddMaskineTilListe(maskine1);
        Kunde.AddMaskineTilListe(maskine2);
        Kunde.AddMaskineTilListe(maskine3);
    }
}