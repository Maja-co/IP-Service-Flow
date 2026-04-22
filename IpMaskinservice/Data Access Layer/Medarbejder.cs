namespace Data_Access_Layer;

public class Medarbejder {
    public string? Id { get; set; }
    public string? MedarbejderNavn { get; set; }
    public string? KodeOrdHash { get; set; }
    public string? Salt{get;set;}
    public string? MailAdresse { get; set; }
    public string? AktivSessionID { get; set; }
    public Medarbejder() { }
    public Medarbejder(string medarbejderId, string medarbejderNavn, string kodeOrdHash, string salt, string mailAdresse) {
        Id = medarbejderId;
        MedarbejderNavn = medarbejderNavn;
        KodeOrdHash = kodeOrdHash;
        Salt = salt;
        MailAdresse = mailAdresse;
    }
}