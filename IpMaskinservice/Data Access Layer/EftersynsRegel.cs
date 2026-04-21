namespace Business_Logic_Layer;

public class EftersynsRegel {
    public int Id { get; set; }
    public string Regel { get; set; }
    public EftersynsRegel() { }
    public EftersynsRegel(string regel) {
        Regel = regel;
    }
}