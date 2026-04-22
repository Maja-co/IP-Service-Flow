using Business_Logic_Layer;
using Data_Access_Layer;
using Xunit;

namespace Test;

public class BusinessLayerTests
{
    // Denne klasse bruges kun til at teste den abstrakte ServiceOpgave
    public class ServiceOpgaveStub : ServiceOpgave
    {
        // Vi implementerer de abstrakte metoder tomt, da vi ikke tester dem her
        public override void afslutOpgave(DateOnly udførtDato, string note) { }
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
            ServiceInterval = ServiceInterval.SeksMåneder
        };

        // Act
        // Her kalder vi metoden i ServiceOpgave, som beregner deadlinen
        serviceOpgave.OpdaterDeadline();

        // Assert
        var forventetDato = sidsteDato.AddMonths(6);
        Assert.Equal(forventetDato, serviceOpgave.Deadline);
    }

    [Fact]
    public void afslutOpgave_AutomatiskWorkflow_OpdatererDeadlineOgOpretterPaamindelse()
    {
        // Arrange
        var maskine = new Maskine { Id = 1 };
        var interval = ServiceInterval.SeksMåneder;
        var udførtDato = new DateOnly(2025, 1, 1);

        // Vi bruger SikkerhedsEftersyn, da det er en konkret klasse
        var eftersyn = new SikkerhedsEftersyn
        {
            Maskine = maskine,
            ServiceInterval = interval
        };

        // Act
        // Vi kalder afslutOpgave. Forventningen er, at denne metode nu 
        // trigger hele workflowet bag kulisserne.
        eftersyn.afslutOpgave(udførtDato, "Service udført efter bogen");

        // Assert
        // 1. Tjek at SidstUdførtDato er opdateret
        Assert.Equal(udførtDato, eftersyn.SidstUdførtDato);

        // 2. Tjek at den næste Deadline er beregnet korrekt (1. juli 2025)
        var forventetNæsteDeadline = udførtDato.AddMonths(6);
        Assert.Equal(forventetNæsteDeadline, eftersyn.Deadline);

        // 3. Tjek at der automatisk er oprettet en påmindelse 14 dage før den nye deadline
        // (1. juli minus 14 dage = 17. juni 2025)
        var forventetPåmindelsesDato = forventetNæsteDeadline.AddDays(-14);

        Assert.NotNull(eftersyn.PåmindelseListe);
        Assert.Contains(eftersyn.PåmindelseListe, p => p.PåmindelsesDato == forventetPåmindelsesDato);
    }
}