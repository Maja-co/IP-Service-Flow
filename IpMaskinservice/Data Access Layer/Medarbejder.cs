namespace Business_Logic_Layer;

public class Medarbejder {
    public string Id { get; set; }
    public string MedarbejderNavn { get; set; }
    public string KodeOrdHash { get; set; }
    public string Salt{get;set;}
    public string MailAdresse { get; set; }
    public Medarbejder() { }
    public Medarbejder(string medarbejderNavn, string kodeOrdHash, string salt, string mailAdresse) {
        MedarbejderNavn = medarbejderNavn;
        KodeOrdHash = kodeOrdHash;
        Salt = salt;
        MailAdresse = mailAdresse;
    }
}