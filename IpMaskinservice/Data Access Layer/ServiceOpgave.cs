namespace Data_Access_Layer;

public abstract class ServiceOpgave {
    public int Id { get; set; } //PK
    public int? MaskineId { get; set; } //FK
    public Maskine? Maskine { get; set; }
    public DateOnly? SidstUdførtDato { get; set; }
    public DateOnly Deadline { get; set; }
    public string? SidstUdførtNote { get; set; }
    public ServiceInterval ServiceInterval { get; set; }
    public Medarbejder? Medarbejder { get; set; }
    public string? MedarbejderId { get; set; } //FK
    public ServiceTeknikker? ServiceTeknikker { get; set; }
    public int? ServiceTeknikkerId { get; set; } //FK
    public MaterialeListe? MaterialeListe { get; set; }
    public int? MaterialeListeId { get; set; } //FK
    public List<Påmindelse>? PåmindelseListe { get; set; }
    protected ServiceOpgave() {}
    protected ServiceOpgave(Maskine maskine, DateOnly sidstUdførtDato, DateOnly deadline, string sidstUdførtNote, 
                            ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker)
    {
        Maskine = maskine;
        MaskineId = maskine?.Id;
        SidstUdførtDato = sidstUdførtDato;
        Deadline = deadline;
        SidstUdførtNote = sidstUdførtNote;
        ServiceInterval = serviceInterval;

        Medarbejder = medarbejder;
        MedarbejderId = medarbejder?.Id;

        ServiceTeknikker = serviceTeknikker;
        ServiceTeknikkerId = serviceTeknikker?.Id;

        MaterialeListe = new MaterialeListe();
        PåmindelseListe = new List<Påmindelse>();
    }
    public abstract void afslutOpgave(DateOnly udførtDato, string note);
    public abstract void createPåmindelse(DateOnly påmindelsesDato);
    public void OpdaterDeadline()
    {
        if (SidstUdførtDato.HasValue)
        {
            Deadline = ServiceInterval switch
            {
                ServiceInterval.ToMåneder => SidstUdførtDato.Value.AddMonths(2),
                ServiceInterval.SeksMåneder => SidstUdførtDato.Value.AddMonths(6),
                ServiceInterval.TolvMåneder => SidstUdførtDato.Value.AddMonths(12),
                _ => Deadline
            };
        }
    }
}