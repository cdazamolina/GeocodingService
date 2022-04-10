using GeocodingService.Core;
using GeocodingService.Infraestructure;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>{ policy.AllowAnyOrigin(); });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Extensiones - Injection Dependencies.
builder.Services.AddCoreServices();
builder.Services.AddInfraestructureServices();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
