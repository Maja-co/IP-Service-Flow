namespace Data_Access_Layer.Models;

public class Maskine {
    public string SerieNummer { get; set; }
    public string Producent { get; set; }
    public Kunde kunde { get; set; }
    public MaskineType maskineType { get; set; }
    public List<AfsluttetService> ServiceHistorikListe { get; set; }
    
}