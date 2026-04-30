namespace Data_Access_Layer.Models;

public class MaterialeListe
{
    public int Id { get; set; }
    public List<MaterialeLinje> MaterialeLinjeListe { get; set; }
    public MaterialeListe()
    {
        MaterialeLinjeListe = new List<MaterialeLinje>();
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
        MaterialeLinjeListe.Add(newMaterialeLinje);
    }
}