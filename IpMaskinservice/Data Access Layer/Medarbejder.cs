namespace Business_Logic_Layer;

public class Medarbejder {
    private string MedarbejderID { get; set; }
    private string MedarbejderNavn { get; set; }
    private string KodeOrdHash { get; set; }
    private string Salt{get;set;}
    private string MailAdresse { get; set; }
    public Medarbejder() { }
    public Medarbejder(string medarbejderID, string medarbejderNavn, string kodeOrdHash, string salt, string mailAdresse) {
        MedarbejderID = medarbejderID;
        MedarbejderNavn = medarbejderNavn;
        KodeOrdHash = kodeOrdHash;
        Salt = salt;
        MailAdresse = mailAdresse;
    }
}