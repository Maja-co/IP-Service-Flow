namespace Business_Logic_Layer;

public class ServiceTeknikker {
    private string TeknikkerNavn { get; set; }
    private string TelefonNummer { get; set; }
    public ServiceTeknikker() { }
    public ServiceTeknikker(string teknikkerNavn, string telefonNummer) {
        TeknikkerNavn = teknikkerNavn;
        TelefonNummer = telefonNummer;
    }
}