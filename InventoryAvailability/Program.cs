using Common.Auth.Middleware;
using Common.Auth.Services;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add authentication using common library
//builder.Services.AddJwtAuthentication("JsonWebTokenTestAdminUser@123456789");
builder.Services.AddJwtAuthentication(
    issuer: "MyIssuer",
    audience: "MyAudience",
    secretKey: "JsonWebTokenTestAdminUser@123456789"
);


builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddScoped<Master.Services.Interfaces.IInventoryService, Master.Services.InventoryService>();
builder.Services.AddScoped<Master.Services.Data.IInventoryRepository, Master.Services.Data.InventoryRepository>();


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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Token is used in the Authorization header.."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

//Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.WithProperty("Service", "ProductService") // Add service identifier
    .WriteTo.File("logs/ProductService-.log", rollingInterval: RollingInterval.Day) // Add file sink
    .CreateLogger();

Serilog.Log.Information("Configuring web host ...");

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication(); // Enable authentication
app.UseAuthorization();  // Enable authorization


// Use the common error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

Serilog.Log.Information("Starting web host ...");
app.Run();
