namespace ClothingStore.WebAPI.Configuration;

public class CorsPolicyConfiguration
{
    public static readonly string SectionName = "CorsPolicies";
    public string Name { get; set; }
    public string[] Origins { get; set; }
}