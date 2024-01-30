namespace APIGateway.Config
{
    public record JwtOptionEntraId(
    string Instance,
    string ClientId,
     string TenantId,
     string Audience,
     string Kid)
    {
        public JwtOptionEntraId() : this(default, default, default, default, default) { }
    }
}
