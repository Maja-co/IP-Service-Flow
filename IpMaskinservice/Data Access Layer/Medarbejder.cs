namespace Business_Logic_Layer;

public class Medarbejder {
    private string MedarbejderID { get; set; }
    private string MedarbejderNavn { get; set; }
    private string KodeOrdHash { get; set; }
    private string Salt{get;set;}
    private string MailAdresse { get; set; }
}