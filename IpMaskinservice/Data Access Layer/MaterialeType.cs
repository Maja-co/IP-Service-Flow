namespace Business_Logic_Layer;

public class MaterialeType {
    private string MaterialeBeskrivelse { get; set; }

    public MaterialeType(){}
    
    public MaterialeType(string materialeBeskrivelse) {
        MaterialeBeskrivelse = materialeBeskrivelse;
    }
};