using Microsoft.EntityFrameworkCore;
using TurnosApp.Infrastructure.Data;
using TurnosApp.Infrastructure.Services;
using TurnosApp.Core.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Registro de Servicios
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Configuración básica de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de Datos
builder.Services.AddDbContext<TurnosDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", policy => 
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddScoped<ITurnoService, TurnoService>();

var app = builder.Build();

// 2. Configuración del Pipeline (Orden Importante)
app.UseSwagger();
app.UseSwaggerUI(); // Esto habilitará la ruta /swagger por defecto

// Inicializar BD
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TurnosDbContext>();
    context.Database.EnsureCreated(); 

    // Insertamos al doctor 1 usando SQL puro para evitar problemas de carpetas
    context.Database.ExecuteSqlRaw(@"
        INSERT OR IGNORE INTO Doctor (Id, Nombre, Especialidad) 
        VALUES (1, 'Dr. Gaston Carranza', 'General');
    ");
}
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();