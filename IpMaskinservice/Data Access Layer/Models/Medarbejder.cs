namespace Data_Access_Layer.Models;
public class Medarbejder {
    public string MedarbejderID { get; set; }
    public string MedarbejderNavn { get; set; }
    public string KodeOrdHash { get; set; }
    public string Salt{get;set;}
    public string MailAdresse { get; set; }
    public string? AktivSessionID { get; set; }
}