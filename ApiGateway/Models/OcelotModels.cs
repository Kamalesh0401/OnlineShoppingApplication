namespace Gateway.Models;

public class OcelotConfiguration
{
    public List<OcelotRoute> Routes { get; set; } = new List<OcelotRoute>();
}

public class OcelotRoute
{
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string UpstreamPathTemplate { get; set; } = string.Empty;
    public List<string> UpstreamHttpMethod { get; set; } = new List<string>();
}
