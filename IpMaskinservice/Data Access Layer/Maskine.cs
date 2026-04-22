using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic_Layer;

public class Maskine
{
    public int Id { get; set; }
    public int? KundeId { get; set; }
    public Kunde? Kunde { get; private set; }
    public string? SerieNummer { get; private set; }
    public string? Producent { get; private set; }
    public MaskineType MaskineType { get; private set; }
    public List<Service> ServiceListe { get; set; }
    public List<SikkerhedsEftersyn> SikkerhedsEftersynListe { get; set; }
    public List<AfsluttetService> ServiceHistorikListe { get; set; }
        // styres af koden og ignoreres af databasen
    [NotMapped]
    public List<ServiceOpgave> ServiceOgEftersynAftalerListe
    {
        get
        {
            var alle = new List<ServiceOpgave>();
            alle.AddRange(ServiceListe);
            alle.AddRange(SikkerhedsEftersynListe);
            return alle;
        }
    }


    internal Maskine()
    {
        ServiceListe = new List<Service>();
        SikkerhedsEftersynListe = new List<SikkerhedsEftersyn>();
        ServiceHistorikListe = new List<AfsluttetService>();
    }
    internal Maskine(string serieNummer, string producent, Kunde kunde, MaskineType maskineType)
    {
        SerieNummer = serieNummer;
        Producent = producent;
        Kunde = kunde;
        KundeId = Kunde.Id;
        MaskineType = maskineType;
        ServiceListe = new List<Service>();
        SikkerhedsEftersynListe = new List<SikkerhedsEftersyn>();
        ServiceHistorikListe = new List<AfsluttetService>();
    }
    //måske de to metoder til create skal gøres anderledes ift. interface. jeg mangler dog noget indspark til hvordan ellers.
    public void createServiceOpgave(ServiceType servicetype, List<OpgaveType> opgaveTypeListe, DateOnly sidstUdførtDato, DateOnly deadline, string sidstUdførtNote, ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker)
    {
        Service nyService = new Service(this, servicetype, opgaveTypeListe, sidstUdførtDato, deadline, sidstUdførtNote, serviceInterval, medarbejder, serviceTeknikker);
        ServiceListe.Add(nyService);
    }
    public void createSikkerhedsEftersyn(List<EftersynsRegel> eftersynsRegel, DateOnly sidstUdførtDato, DateOnly deadline, string sidstUdførtNote, ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker)
    {
        SikkerhedsEftersyn nytEftersyn = new SikkerhedsEftersyn(this, eftersynsRegel, sidstUdførtDato, deadline, sidstUdførtNote, serviceInterval, medarbejder, serviceTeknikker);
        SikkerhedsEftersynListe.Add(nytEftersyn);
    }
    public void addAfsluttetService(AfsluttetService afsluttet)
    {
        ServiceHistorikListe.Add(afsluttet);
    }
}