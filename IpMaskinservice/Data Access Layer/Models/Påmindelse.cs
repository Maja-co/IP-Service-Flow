namespace Data_Access_Layer.Models;

public class Påmindelse {
    public int Id { get; set; }
    public DateOnly PåmindelsesDato { get; set; }
    public ServiceOpgave? ServiceOpgave { get; set; }
    public int? ServiceOpgaveId { get; set; }
    public Påmindelse() { }
    public Påmindelse(DateOnly påmindelsesDato, ServiceOpgave serviceOpgave) {
        PåmindelsesDato = påmindelsesDato;
        ServiceOpgave = serviceOpgave;
        ServiceOpgaveId = serviceOpgave?.Id;
    }
    //TODO implementer mail på den her
}