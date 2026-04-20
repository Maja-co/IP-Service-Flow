namespace Business_Logic_Layer;

public class Maskine {
    private string SerieNummer { get; set; }
    private string Producent { get; set; }
    private Kunde kunde { get; set; }
    private MaskineType maskineType { get; set; }
    private List<IServiceOpgave> ServiceOgEftersynAftalerListe { get; set; }
    private List<AfsluttetService> ServiceHistorikListe { get; set; }
    
    internal Maskine() { }
    internal Maskine(string serieNummer, string producent, Kunde kunde, MaskineType maskineType) {
        SerieNummer = serieNummer;
        Producent = producent;
        this.kunde = kunde;
        this.maskineType = maskineType;
        ServiceOgEftersynAftalerListe = new List<IServiceOpgave>();
        ServiceHistorikListe = new List<AfsluttetService>();
    }
    //måske de to metoder til create skal gøres anderledes ift. interface. jeg mangler dog noget indspark til hvordan ellers.
    public void createServiceOpgave(ServiceType servicetype, List<OpgaveType> opgaveTypeListe, DateOnly sidstUdførtDato, DateOnly deadline, string sidstUdførtNote, ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker)
    {
        IServiceOpgave nyService = new Service(this, servicetype, opgaveTypeListe, sidstUdførtDato, deadline, sidstUdførtNote, serviceInterval, medarbejder, serviceTeknikker);
        ServiceOgEftersynAftalerListe.Add(nyService);
    }
    public void createSikkerhedsEftersyn(List<EftersynsRegel> eftersynsRegel, DateOnly sidstUdførtDato, DateOnly deadline, string sidstUdførtNote, ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker)
    {
        IServiceOpgave nytEftersyn = new SikkerhedsEftersyn(this, eftersynsRegel, sidstUdførtDato, deadline, sidstUdførtNote, serviceInterval, medarbejder, serviceTeknikker);
        ServiceOgEftersynAftalerListe.Add(nytEftersyn);
    }
    public void addAfsluttetService(AfsluttetService afsluttet) {
        ServiceHistorikListe.Add(afsluttet);
    }
}