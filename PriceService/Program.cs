var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<Master.Services.Interfaces.IPriceService, Master.Services.PriceService>();
builder.Services.AddScoped<Master.Services.Data.IPriceRepository, Master.Services.Data.PriceRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
