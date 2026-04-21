namespace Business_Logic_Layer;

public class MaterialeLinje {
    public int Id { get; set; }
    public int Antal { get; set; }
    public string information { get; set; }
    public MaterialeType MaterialeType { get; set; }
    public int? MatrialeTypeId { get; set; }

    internal MaterialeLinje() { }
    internal MaterialeLinje(int antal, string information, MaterialeType materialeType) {
        Antal = antal;
        this.information = information;
        MaterialeType = materialeType;
        MatrialeTypeId = materialeType?.Id;
    }
}