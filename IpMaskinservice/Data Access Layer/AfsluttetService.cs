namespace Business_Logic_Layer;

public class AfsluttetService {
    public int Id { get; set; }
    public string Note{ get; set; }
    public DateOnly UdførtDato { get; set; }
    public ServiceOpgave ServiceOpgave { get; set; }
    public int? ServiceOpgaveId { get; set; }

    internal AfsluttetService() { }
    internal AfsluttetService(DateOnly udførtDato, string note, ServiceOpgave opgave) {
        UdførtDato = udførtDato;
        Note = note;
        ServiceOpgave = opgave;
        ServiceOpgaveId = ServiceOpgave?.Id;
    }
}