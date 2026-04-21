namespace Data_Access_Layer;

public class MaterialeListe
{
    private List<MaterialeLinje> materialeLinjeListe { get; set; }
    internal MaterialeListe()
    {
        this.materialeLinjeListe = new List<MaterialeLinje>();
    }

    public void createMaterialeLinje(int antal, string information, MaterialeType materialeType)
    {
        //TODO : Validering af input m. try/cath til uI
        if (antal <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(antal), "Antal skal være større end 0.");
        }
        if (string.IsNullOrWhiteSpace(information))
        {
            throw new ArgumentException("Information må ikke være tom.", nameof(information));
        }
        MaterialeLinje newMaterialeLinje = new MaterialeLinje(antal, information, materialeType);
        materialeLinjeListe.Add(newMaterialeLinje);
    }
}