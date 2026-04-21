namespace Data_Access_Layer;

public class AfsluttetService {
    private string Note{ get; set; }
    private DateOnly UdførtDato { get; set; }
    private IServiceOpgave iServiceOpgave { get; set; }

    internal AfsluttetService() { }
    internal AfsluttetService(DateOnly udførtDato, string note, IServiceOpgave opgave) {
        UdførtDato = udførtDato;
        Note = note;
        iServiceOpgave = opgave;
    }
}