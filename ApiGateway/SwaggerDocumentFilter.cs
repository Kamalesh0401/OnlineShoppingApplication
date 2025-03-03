using Gateway.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace Gateway;

public class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply( OpenApiDocument swaggerDoc, DocumentFilterContext context )
    {
        var paths = new OpenApiPaths();

        // Load Ocelot configuration from ocelot.json
        var ocelotConfig = LoadOcelotConfig();

        foreach (var route in ocelotConfig.Routes)
        {
            paths.Add(route.UpstreamPathTemplate, new OpenApiPathItem
            {
                Operations = new Dictionary<OperationType, OpenApiOperation>
                {
                    [GetOperationType(route.UpstreamHttpMethod)] = new OpenApiOperation
                    {
                        Tags = new List<OpenApiTag> { new OpenApiTag { Name = route.Name } },
                        Summary = route.Summary,
                        Responses = new OpenApiResponses
                        {
                            ["200"] = new OpenApiResponse { Description = "Successful response" }
                        }
                    }
                }
            });
        }

        swaggerDoc.Paths = paths;
    }

    private static OcelotConfiguration LoadOcelotConfig()
    {
        var configPath = Path.Combine(AppContext.BaseDirectory, "ocelot.json");
        var json = File.ReadAllText(configPath);
        return JsonSerializer.Deserialize<OcelotConfiguration>(json)!;
    }

    private static OperationType GetOperationType( List<string> methods )
    {
        // Currently handling only GET, can be extended for other HTTP methods
        return methods.Contains("GET") ? OperationType.Get : OperationType.Get;
    }
}


