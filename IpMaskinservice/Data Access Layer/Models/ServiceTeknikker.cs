namespace Data_Access_Layer.Models;

public class ServiceTeknikker {
    public int Id { get; set; }
    public string? TeknikkerNavn { get; set; }
    public string? TelefonNummer { get; set; }
    public ServiceTeknikker() { }
    public ServiceTeknikker(string teknikkerNavn, string telefonNummer) {
        TeknikkerNavn = teknikkerNavn;
        TelefonNummer = telefonNummer;
    }
}