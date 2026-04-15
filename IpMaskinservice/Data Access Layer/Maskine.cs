namespace Business_Logic_Layer;

public class Maskine {
    private string SerieNummer { get; set; }
    private string Producent { get; set; }
    private Kunde kunde { get; set; }
    private MaskineType maskineType { get; set; }
    private List<AfsluttetService> ServiceHistorikListe { get; set; }
    
}