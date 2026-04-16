namespace Business_Logic_Layer;

public class Service : IServiceOpgave{
    public DateOnly SidstUdførtDato { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateOnly Deadline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string SidstUdførtNote { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public ServiceInterval ServiceInterval { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Medarbejder Medarbejder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public ServiceTeknikker serviceTeknikker { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    private ServiceType servicetype { get; set; }
    private List<OpgaveType> OpgaveTypeListe { get; set; }

    public void afslutOpgave(DateOnly udførtDato, string note)
    {
        throw new NotImplementedException();
    }

    public void createMaterialeListe()
    {
        throw new NotImplementedException();
    }

    public void createPåmindelse(DateOnly påmindelsesDato)
    {
        throw new NotImplementedException();
    }
}