using Business_Logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using Xunit;

namespace Test;

public class MaskineIntegraionTests
{
    //US2.1-2.6 (Logik og validering af tilknytning i hukommelsen)
    [Fact]
    public void OpretMaskineForKundeTest()
    {

        // Arrange
        var options = new DbContextOptionsBuilder<MaskinContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var db = new MaskinContext(options);
        var maskineRepo = new MaskineRepository(db);
        var kundeRepo = new KundeRepository(db);
        var manager = new MaskineManager(maskineRepo, kundeRepo);

        //opret en testkunde og opret en testmaskine
        var testKunde = new Data_Access_Layer.Models.Kunde("Test Firma", "Gade 1 ", "Person", "12345678", "mail@test.dk", true, 12345678);
        var testMaskine = new Maskine("SN-999", "TestBrand", testKunde, MaskineType.Savning);

        // Act-Udfører selve tilknytningen via manageren
        manager.OpretMaskineForKunde(testMaskine, testKunde);

        // Assert-Bekræfter at maskinen nu findes i kundens liste
        Assert.NotNull(testKunde);
        //Assert.NotNull(testMaskine);

        // Tjek om forbindelsen er oprettet korrekt i listen
        Assert.Contains(testMaskine, testKunde.MaskineListe);

    }

    //US2.11 (Logik til gruppering af maskiner efter fabrikant)
    [Fact]
    public void TestGruppering()
    {

        //Arrange
        var options = new DbContextOptionsBuilder<MaskinContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var db = new MaskinContext(options);
        var maskineRepo = new MaskineRepository(db);
        var kundeRepo = new KundeRepository(db);
        var manager = new MaskineManager(maskineRepo, kundeRepo);
        var kunde = new Kunde("Test Firma 1", "Gade 1 ", "Person 1", "12345678", "mail@test.dk", true, 12345678);

        //Act
        var maskine1 = new Maskine("SN-001", "Volvo", kunde, MaskineType.Savning);
        var maskine2 = new Maskine("SN-002", "Volvo", kunde, MaskineType.Boring);
        var maskine3 = new Maskine("SN-003", "Scania", kunde, MaskineType.Rørbukning);

        //Assert
        kunde.MaskineListe.Add(maskine1);
        kunde.MaskineListe.Add(maskine2);
        kunde.MaskineListe.Add(maskine3);

        // Act - Kalder grupperingslogikken
        var grupperet = manager.GrupperMaskinerEfterProducent(kunde);

        // Assert - Bekræfter at vi har 2 grupper (Volvo og Scania)
        Assert.Equal(2, grupperet.Count);
        Assert.True(grupperet.ContainsKey("Volvo"));
        Assert.True(grupperet.ContainsKey("Scania"));
    }

    //US2.7 Database integration - oprettelse af rækker i SQL

    [Fact]
    public void DatabaseTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MaskinContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var db = new MaskinContext(options);
        var kundeRepo = new KundeRepository(db);
        var maskineRepo = new MaskineRepository(db);


        // Opret en testkunde og en testmaskine
        var testKunde = new Kunde("Dansk Maskine A/S", "Aarhusvej 10 ", "Mads Jensen", "88888888", "mads@mads.dk", true, 12345678);
        kundeRepo.Add(testKunde);

        //Opret en testmaskine og tilføj den til kunden
        var testMaskine = new Maskine("SN-999-XYZ", "Volvo", testKunde, new MaskineType());
        maskineRepo.Add(testMaskine);

        // Assert - Hvis Id er over 0, betyder det at SQL har modtaget og gemt data korrekt
        Assert.True(testKunde.Id > 0);
        Assert.True(testMaskine.Id > 0);

    }

}