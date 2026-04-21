using Business_Logic_Layer;
using Data_Access_Layer;
using Xunit; 
namespace Test;

public class UnitTest1 {
    [Fact]
    public void OpretMaskineForKundeTest() {

        // Arrange
        var manager = new MaskineManager();

        //opret en testkunde og opret en testmaskine
        var testKunde = new Kunde("Test Firma", "Gade 1 ", "Person", "12345678", "mail@test.dk", true, 123456);
        var testMaskine = new Maskine("SN-999", "TestBrand", testKunde,MaskineType.Savning);

        // Act
        manager.OpretMaskineForKunde(testMaskine, testKunde);

        // Assert
        Assert.NotNull(testKunde);
        Assert.NotNull(testMaskine);

    }
    [Fact]
    public void TestGruppering() { 
        var manager = new MaskineManager();
    }
}