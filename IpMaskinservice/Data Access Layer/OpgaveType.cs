namespace Business_Logic_Layer;

public class OpgaveType {
    public int Id { get; set; }
    public string OpgaveBeskrivelse { get; set; }
    public OpgaveType() { }
    public OpgaveType(string OpgaveBeskrivelse)
    {
        this.OpgaveBeskrivelse = OpgaveBeskrivelse;
    }
}