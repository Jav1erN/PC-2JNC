namespace PC_2JNC.Infrastructure.Authentication;

public sealed class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public int ExpirationMinutes { get; init; } = 60;
    public string DemoUsername { get; init; } = "admin";
    public string DemoPassword { get; init; } = "admin123";
}
