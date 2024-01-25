namespace APIGateway.Config
{
    public record JwtOptions(
        string Issuer,
        string Audience,
        string SigningKey,
        int ExpirationSeconds
    )
    {
         public JwtOptions() : this(default, default,default,default) { }
    }
}
