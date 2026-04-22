using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;

namespace Data_Access_Layer.Models;

public class Maskine
{
    public int Id { get; set; }
    public int? KundeId { get; set; }
    public Kunde? Kunde { get; private set; }
    public string? SerieNummer { get; private set; }
    public string? Producent { get; private set; }
    public MaskineType MaskineType { get; private set; }
    public List<AfsluttetService> ServiceHistorikListe { get; set; }
    public List<ServiceOpgave> ServiceOgEftersynAftalerListe { get; set; } = new List<ServiceOpgave>();

    [NotMapped]
    public List<Service> ServiceListe => ServiceOgEftersynAftalerListe.OfType<Service>().ToList();

    [NotMapped]
    public List<SikkerhedsEftersyn> SikkerhedsEftersynListe => ServiceOgEftersynAftalerListe.OfType<SikkerhedsEftersyn>().ToList();


    internal Maskine()
    {
        ServiceOgEftersynAftalerListe = new List<ServiceOpgave>();
        ServiceHistorikListe = new List<AfsluttetService>();
    }
    internal Maskine(string serieNummer, string producent, Kunde kunde, MaskineType maskineType)
    {
        SerieNummer = serieNummer;
        Producent = producent;
        Kunde = kunde;
        KundeId = Kunde.Id;
        MaskineType = maskineType;
        ServiceHistorikListe = new List<AfsluttetService>();
    }
    //måske de to metoder til create skal gøres anderledes ift. interface. jeg mangler dog noget indspark til hvordan ellers.
    public void createServiceOpgave(ServiceType servicetype, List<OpgaveType> opgaveTypeListe, DateOnly sidstUdførtDato, DateOnly deadline, string sidstUdførtNote, ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker)
    {
        Service nyService = new Service(this, servicetype, opgaveTypeListe, sidstUdførtDato, deadline, sidstUdførtNote, serviceInterval, medarbejder, serviceTeknikker);
        ServiceOgEftersynAftalerListe.Add(nyService);
    }
    public void createSikkerhedsEftersyn(List<EftersynsRegel> eftersynsRegel, DateOnly sidstUdførtDato, DateOnly deadline, string sidstUdførtNote, ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker)
    {
        SikkerhedsEftersyn nytEftersyn = new SikkerhedsEftersyn(this, eftersynsRegel, sidstUdførtDato, deadline, sidstUdførtNote, serviceInterval, medarbejder, serviceTeknikker);
        ServiceOgEftersynAftalerListe.Add(nytEftersyn);
    }
    public void addAfsluttetService(AfsluttetService afsluttet)
    {
        ServiceHistorikListe.Add(afsluttet);
    }
}