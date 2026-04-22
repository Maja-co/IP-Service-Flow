namespace Data_Access_Layer;

public class MaterialeType {
    public int Id { get; set; }
    public string? MaterialeBeskrivelse { get; set; }

    public MaterialeType(){}
    
    public MaterialeType(string materialeBeskrivelse) {
        MaterialeBeskrivelse = materialeBeskrivelse;
    }
};