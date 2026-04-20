namespace Business_Logic_Layer;

public class MaterialeLinje {
    private int Antal { get; set; }
    private string information { get; set; }
    private MaterialeType materialeType { get; set; }

    internal MaterialeLinje() { }
    internal MaterialeLinje(int antal, string information, MaterialeType materialeType) {
        Antal = antal;
        this.information = information;
        this.materialeType = materialeType;
    }
}