using GeocodingService.Core;
using GeocodingService.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Extensiones - Injection Dependencies.
builder.Services.AddCoreServices();
builder.Services.AddInfraestructureServices();

var app = builder.Build();
app.UseCors(builder => builder
     .WithOrigins("*")
     .AllowAnyMethod()
     .SetIsOriginAllowed((host) => true)
     .AllowAnyHeader());
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();
app.Run();
