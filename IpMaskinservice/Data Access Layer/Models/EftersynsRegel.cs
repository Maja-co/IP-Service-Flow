namespace Data_Access_Layer.Models;

public class EftersynsRegel {
    public int Id { get; set; }
    public string? Regel { get; set; }
    public EftersynsRegel() { }
    public EftersynsRegel(string regel) {
        Regel = regel;
    }
}