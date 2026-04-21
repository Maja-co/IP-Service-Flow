namespace Data_Access_Layer;

public class OpgaveType {
    private string OpgaveBeskrivelse { get; set; }
    public OpgaveType() { }
    public OpgaveType(string OpgaveBeskrivelse)
    {
        this.OpgaveBeskrivelse = OpgaveBeskrivelse;
    }
}