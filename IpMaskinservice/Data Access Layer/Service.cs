namespace Data_Access_Layer;

public class Service : IServiceOpgave
{
    private ServiceType servicetype { get; set; }
    private List<OpgaveType> OpgaveTypeListe { get; set; }
    public DateOnly SidstUdførtDato { get; set; }
    public DateOnly Deadline { get; set; }
    public string SidstUdførtNote { get; set; }
    public ServiceInterval ServiceInterval { get; set; }
    public Medarbejder Medarbejder { get; set; }
    public ServiceTeknikker serviceTeknikker { get; set; }
    public MaterialeListe MaterialeListe { get; set; }
    public List<Påmindelse> PåmindelseListe { get; set; }
    public Maskine Maskine { get; set; }

    public Service() { }
    public Service(Maskine maskine, ServiceType servicetype, List<OpgaveType> opgaveTypeListe, DateOnly sidstUdførtDato, DateOnly deadline, string sidstUdførtNote, ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker)
    {
        this.Maskine = maskine;
        this.servicetype = servicetype;
        OpgaveTypeListe = opgaveTypeListe;
        SidstUdførtDato = sidstUdførtDato;
        Deadline = deadline;
        SidstUdførtNote = sidstUdførtNote;
        ServiceInterval = serviceInterval;
        Medarbejder = medarbejder;
        this.serviceTeknikker = serviceTeknikker;
        MaterialeListe = new MaterialeListe();
        PåmindelseListe = new List<Påmindelse>();
    }
    public void afslutOpgave(DateOnly udførtDato, string note)
    {
        //TODO implementer try/catch i UI-laget, så det ikke er nødvendigt at håndtere det her
        if (string.IsNullOrWhiteSpace(note))
        {
            throw new ArgumentException("Der skal tilføjes en note, når en opgave afsluttes.");
        }
        if (udførtDato > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Udført dato kan ikke være i fremtiden.");
        }
        this.SidstUdførtDato = udførtDato;
        this.SidstUdførtNote = note;

        AfsluttetService afsluttet = new AfsluttetService(udførtDato, note, this);
        Maskine.addAfsluttetService(afsluttet);
    }
    public void createPåmindelse(DateOnly påmindelsesDato)
    {
        if (påmindelsesDato < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Påmindelsesdatoen må ikke være i fortiden.");
        }
        Påmindelse newPåmindelse = new Påmindelse(påmindelsesDato, this);
    }

    public void addOpgaveType(OpgaveType opgaveType)
    {
        OpgaveTypeListe.Add(opgaveType);
    }
}