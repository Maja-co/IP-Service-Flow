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

    public void createServiceOpgave()
    {
        //TODO 
    }
    public void addAfsluttetService(DateTime serviceDato, string information, MaterialeListe materialeListe) {
        //TODO : Validering af input m. try/cath til uI
        if (serviceDato > DateTime.Now)
        {
            throw new ArgumentOutOfRangeException(nameof(serviceDato), "Service dato kan ikke være i fremtiden.");
        }
        if (string.IsNullOrWhiteSpace(information))
        {
            throw new ArgumentException("Information må ikke være tom.", nameof(information));
        }
        AfsluttetService newAfsluttetService = new AfsluttetService(serviceDato, information, materialeListe);
        ServiceHistorikListe.Add(newAfsluttetService);
    }
}