//using Data_Access_Layer;

//namespace Test;

//public class BusinessLayerTests {
//    [Fact]
//    public void BeregnNaesteDeadline_VedSeksMaaneder_ReturnererKorrektDato() {
//        // Arrange: Opsætning af testdata baseret på klassediagrammet.
//        var interval = ServiceInterval.SEKSMÅNEDER; // Fra ServiceInterval
//        var sidsteDato = new DateTime(2025, 1, 1); // SidsteUdførtDato

//        var serviceOpgave = new ServiceOpgave();
//        {
//            sidstUdførtDato = sidsteDato,
//            Interval = interval
//        };

//        // Act: Handlingen der testes
//        serviceOpgave.OpdaterDeadline();

//        // Assert: Verificeringen af testen
//        var forventetDato = sidsteDato.AddMonths(6);
//        Assert.Equal(forventetDato, serviceOpgave.deadline);
//    }

//    public void AfslutSikkerhedsEftersyn_UdenAlleReglerTjekket_KasterException(){
//        // Arrange:
//        var eftersyn = new SikkerhedsEftersyn(); // Specialisering af IServiceOpgave
//        var regel1 = new EftersynsRegel { regel = "Nødstop" };
//        var regel2 = new EftersysnsRegel { regel = "Afskærming" };

//        eftersyn.Regler.Add(regel1);
//        eftersyn.Regler.Add(regel2);

//        // Simulering af at teknikeren ikke har godkent alle regler
//        var tjekkedeRegler = new List<EftersynsRegel> { regel1 };
//        var teknikker = new ServiceTekniker { navn = "Mads" };

//        // Act og Assert:
//        Assert.Throws<InvalidOperationException>(() =>
//            eftersyn.Afslut(tekniker, tjekkedeRegler)
//            );
//    }
//}