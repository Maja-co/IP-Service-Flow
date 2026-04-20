using Microsoft.VisualBasic;

namespace Business_Logic_Layer;

public class Kunde
{
    private string FirmaNavn { get; set; }
    private string Adresse { get; set; }
    private string KontaktPersonNavn { get; set; }
    private string KontaktPersonTelefonnummer { get; set; }
    private string MailAdresse { get; set; }
    private bool ErAktiv { get; set; }
    private int CvrNummer { get; set; }
    private List<Maskine> MaskineListe { get; set; }

    public Kunde() { }
    public Kunde(string firmaNavn, string adresse, string kontaktPersonNavn, string kontaktPersonTelefonnummer, string mailAdresse, bool erAktiv, int cvrNummer)
    {
        FirmaNavn = firmaNavn;
        Adresse = adresse;
        KontaktPersonNavn = kontaktPersonNavn;
        KontaktPersonTelefonnummer = kontaktPersonTelefonnummer;
        MailAdresse = mailAdresse;
        ErAktiv = erAktiv;
        CvrNummer = cvrNummer;
        MaskineListe = new List<Maskine>();
    }

    public void createMaskine(string serieNummer, string producent, Kunde kunde, MaskineType maskineType)
    {
        //TODO : Validering af input m. try/cath til uI
        if (string.IsNullOrWhiteSpace(serieNummer))
        {
            throw new ArgumentException("Serienummer må ikke være tom.", nameof(serieNummer));
        }
        if (string.IsNullOrWhiteSpace(producent))
        {
            throw new ArgumentException("Producent må ikke være tom.", nameof(producent));
        }
        Maskine newMaskine = new Maskine(serieNummer, producent, kunde, maskineType);
        MaskineListe.Add(newMaskine);
    }
}