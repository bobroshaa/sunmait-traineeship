namespace ClothingStore.WebAPI.Configuration;

public class JwtConfiguration
{
    public static readonly string SectionName = "JwtConfig";
    public string Secret { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
}