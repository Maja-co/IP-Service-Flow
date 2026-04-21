namespace Business_Logic_Layer;

public class SikkerhedsEftersyn : ServiceOpgave
{
    public List<EftersynsRegel> EftersynsRegelListe { get; set; } = new List<EftersynsRegel>();
    public SikkerhedsEftersyn(){ }
    public SikkerhedsEftersyn(Maskine maskine, List<EftersynsRegel> eftersynsRegel, DateOnly sidstUdførtDato, DateOnly deadline, 
                              string sidstUdførtNote, ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker)
        : base(maskine, sidstUdførtDato, deadline, sidstUdførtNote, serviceInterval, medarbejder, serviceTeknikker)
    {
        EftersynsRegelListe = eftersynsRegel;
    }
    public override void afslutOpgave(DateOnly udførtDato, string note)
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
    public override void createPåmindelse(DateOnly påmindelsesDato)
    {
        if (påmindelsesDato < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Påmindelsesdatoen må ikke være i fortiden.");
        }
        Påmindelse newPåmindelse = new Påmindelse(påmindelsesDato, this);
        PåmindelseListe.Add(newPåmindelse);
    }
    public void addEftersynsRegel(EftersynsRegel eftersynsRegel)
    {
        EftersynsRegelListe.Add(eftersynsRegel);
    }
}