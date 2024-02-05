using OidcProxy.Net.ModuleInitializers;
namespace Host.Config;

public class EntraIdProxyConfig : ProxyConfig
{
    public EntraIdConfig EntraId { get; set; }
}