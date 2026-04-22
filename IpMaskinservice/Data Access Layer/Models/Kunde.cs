using Microsoft.VisualBasic;

namespace Data_Access_Layer.Models;

public class Kunde
{
    public int Id { get; set; }
    public string? FirmaNavn { get; private set; }
    public string? Adresse { get; private set; }
    public string? KontaktPersonNavn { get; private set; }
    public string? KontaktPersonTelefonnummer { get; private set; }
    public string? MailAdresse { get; private set; }
    public bool ErAktiv { get; private set; }
    public int CvrNummer { get; private set; }
    public List<Maskine> MaskineListe { get; private set; }

    public Kunde()
    {
        MaskineListe = new List<Maskine>();
    }
    public Kunde(string firmaNavn, string adresse, string kontaktPersonNavn, string kontaktPersonTelefonnummer, string mailAdresse, bool erAktiv, int cvrNummer)
    {
        if (string.IsNullOrWhiteSpace(firmaNavn))
        {
            throw new ArgumentException("Firmanavn skal udfyldes.", nameof(firmaNavn));
        }
        if (string.IsNullOrWhiteSpace(kontaktPersonTelefonnummer) || string.IsNullOrWhiteSpace(mailAdresse))
        {
            throw new ArgumentException("Både telefonnummer og mailadresse skal udfyldes.");
        }
        if (cvrNummer < 10000000 || cvrNummer > 99999999)
        {
            throw new ArgumentException("CVR-nummeret skal være præcis 8 cifre langt.", nameof(cvrNummer));
        }
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
        Maskine newMaskine = new Maskine(serieNummer, producent, this, maskineType); 
        MaskineListe.Add(newMaskine);
    }

    public void Deaktiver()
    {
        ErAktiv = false;
        //TODO - tjek op på, hvordan vi skal gøre i vores DB
        // overvej at tilføje logik der f.eks. også deaktiverer fremtidige påmindelser,
    }
}