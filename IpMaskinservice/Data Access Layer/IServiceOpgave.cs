namespace Business_Logic_Layer;

public interface IServiceOpgave {
    public DateOnly SidstUdførtDato { get; set; }
    public DateOnly Deadline { get; set; }
    public string SidstUdførtNote { get; set; }
    public ServiceInterval ServiceInterval { get; set; }
    public Medarbejder Medarbejder { get; set; }
    public ServiceTeknikker serviceTeknikker { get; set; }
    
}