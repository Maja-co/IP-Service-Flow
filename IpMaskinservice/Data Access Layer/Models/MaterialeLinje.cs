namespace Data_Access_Layer.Models;

public class MaterialeLinje {
    public int Id { get; set; }
    public int Antal { get; set; }
    public string? Information { get; set; }
    public MaterialeType? MaterialeType { get; set; }
    public int? MatrialeTypeId { get; set; }

    internal MaterialeLinje() { }
    internal MaterialeLinje(int antal, string information, MaterialeType materialeType) {
        Antal = antal;
        Information = information;
        MaterialeType = materialeType;
        MatrialeTypeId = materialeType?.Id;
    }
}