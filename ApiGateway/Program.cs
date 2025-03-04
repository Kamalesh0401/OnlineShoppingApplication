using Gateway;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure Ocelot with JSON configuration file
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// ✅ Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// ✅ Add Controllers (Required for Swagger)
builder.Services.AddControllers();

// ✅ Add SwaggerGen with proper configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gateway API",
        Version = "v1",
        Description = "API Gateway that routes requests to microservices"
    });
});

// ✅ Add Ocelot services
builder.Services.AddOcelot();

var app = builder.Build();

// ✅ Apply CORS **before** Swagger
app.UseCors("AllowAllOrigins");

// ✅ Ensure Swagger Middleware is registered before Ocelot
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API v1");
        c.SwaggerEndpoint("https://localhost:7001/swagger/v1/swagger.json", "Product Service API");
        c.SwaggerEndpoint("https://localhost:7120/swagger/v1/swagger.json", "Inventory Service API");
        c.SwaggerEndpoint("https://localhost:7248/swagger/v1/swagger.json", "Price Service API");
        c.SwaggerEndpoint("https://localhost:7233/swagger/v1/swagger.json", "User Service API");
    });
}

// ✅ Ensure Controllers are mapped before Ocelot
app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

// ✅ Apply Ocelot middleware last
await app.UseOcelot();

app.Run();
