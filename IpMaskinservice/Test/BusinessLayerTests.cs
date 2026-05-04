using Data_Access_Layer.Models;


namespace Test;

public class BusinessLayerTests
{
    // Denne klasse bruges kun til at teste den abstrakte ServiceOpgave
    public class ServiceOpgaveStub : ServiceOpgave
    {
        // Vi implementerer de abstrakte metoder tomt, da vi ikke tester dem her
        public override void afslutOpgave(DateOnly udførtDato, string note)
        {
            this.SidstUdførtDato = udførtDato;
            this.SidstUdførtNote = note;

            // Kald de universelle metoder i ServiceOpgave
            this.OpdaterDeadline();

            // Automatisk påmindelse logik
            DateOnly påmindelse = this.Deadline.AddDays(-30);
            this.createPåmindelse(påmindelse);
        }
        public override void createPåmindelse(DateOnly påmindelsesDato) { }

       
           
    }
    [Fact]
    public void BeregnNaesteDeadline_VedSeksMaaneder_ReturnererKorrektDato()
    {
        // Arrange
        var sidsteDato = new DateOnly(2025, 1, 1);

        // Vi bruger vores stub i stedet for den abstrakte klasse
        var serviceOpgave = new ServiceOpgaveStub
        {
            SidstUdførtDato = sidsteDato,
            ServiceInterval = ServiceInterval.SEKSMÅNEDER
        };

        // Act
        // Her kalder vi metoden i ServiceOpgave, som beregner deadlinen
        serviceOpgave.OpdaterDeadline();

        // Assert
        var forventetDato = sidsteDato.AddMonths(6);
        Assert.Equal(forventetDato, serviceOpgave.Deadline);
    }


    [Fact]
    public void afslutOpgave_BasisWorkflow_OpdatererDeadlineOgOpretterPaamindelse()
    {
        // Arrange
        var interval = ServiceInterval.SEKSMÅNEDER;
        var udførtDato = new DateOnly(2025, 1, 1);

        // Vi tester direkte på stubben (vores repræsentant for ServiceOpgave)
        var opgave = new ServiceOpgaveStub
        {
            ServiceInterval = interval
        };

        // Act
        opgave.afslutOpgave(udførtDato, "Standard service udført");

        // Assert
        // 1. Er deadlinen beregnet korrekt (01-07-2025)?
        var forventetDeadline = udførtDato.AddMonths(6);
        Assert.Equal(forventetDeadline, opgave.Deadline);

        // 2. Er der oprettet en påmindelse i listen?
        Assert.NotEmpty(opgave.PåmindelseListe);

        // 3. Ligger påmindelsen 30 dage før den nye deadline?
        var forventetPåmindelsesDato = forventetDeadline.AddDays(-30);
        Assert.Contains(opgave.PåmindelseListe, p => p.PåmindelsesDato == forventetPåmindelsesDato);
    }
}