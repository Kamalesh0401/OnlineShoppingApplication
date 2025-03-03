using Gateway;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Ocelot json file
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot to service collection
builder.Services.AddOcelot();

// Add Swagger for Gateway documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gateway API",
        Version = "v1",
        Description = "A Gateway API for microservices",
    });

    // Define the microservices endpoints
    c.AddServer(new OpenApiServer
    {
        Url = "https://localhost:5000",
        Description = "Gateway Server"
    });

    // Add routes documentation
    c.DocInclusionPredicate((docName, apiDesc) => true);
    c.DocumentFilter<SwaggerDocumentFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API V1");
    });
}

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

await app.UseOcelot();

app.Run();