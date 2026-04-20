namespace Business_Logic_Layer;

public class Påmindelse {
    private DateOnly PåmindelsesDato { get; set; }
    public IServiceOpgave serviceOpgave { get; set; }
    internal Påmindelse() { }
    internal Påmindelse(DateOnly påmindelsesDato, IServiceOpgave serviceOpgave) {
        PåmindelsesDato = påmindelsesDato;
        this.serviceOpgave = serviceOpgave;
    }
    //TODO implementer mail på den her
}