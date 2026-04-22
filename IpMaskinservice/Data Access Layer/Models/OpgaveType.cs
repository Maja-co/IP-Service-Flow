namespace Data_Access_Layer.Models;

public class OpgaveType {
    public int Id { get; set; }
    public string? OpgaveBeskrivelse { get; set; }
    public OpgaveType() { }
    public OpgaveType(string OpgaveBeskrivelse)
    {
        this.OpgaveBeskrivelse = OpgaveBeskrivelse;
    }
}