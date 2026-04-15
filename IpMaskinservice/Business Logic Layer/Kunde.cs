namespace Business_Logic_Layer;

public class Kunde {
    private string FirmaNavn { get; set; }
    private string Adresse { get; set; }
    private string KontaktPersonNavn { get; set; }
    private string KontaktPersonTelefonnummer { get; set; }
    private string MailAdresse { get; set; }
    private bool ErAktiv { get; set; }
    private int CvrNummer { get; set; }
    private List<Maskine> MaskineListe { get; set; }
    
}