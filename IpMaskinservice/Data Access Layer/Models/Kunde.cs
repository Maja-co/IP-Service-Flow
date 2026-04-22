namespace Data_Access_Layer.Models;

public class Kunde {
    public string FirmaNavn { get; set; }
    public string Adresse { get; set; }
    public string KontaktPersonNavn { get; set; }
    public string KontaktPersonTelefonnummer { get; set; }
    public string MailAdresse { get; set; }
    public bool ErAktiv { get; set; }
    public int CvrNummer { get; set; }
    public List<Maskine> MaskineListe { get; set; }
    
}