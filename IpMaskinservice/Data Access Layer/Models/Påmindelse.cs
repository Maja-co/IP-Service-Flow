namespace Data_Access_Layer.Models;

public class Påmindelse {
    public DateOnly PåmindelsesDato { get; set; }
    public IServiceOpgave serviceOpgave { get; set; }
    
}