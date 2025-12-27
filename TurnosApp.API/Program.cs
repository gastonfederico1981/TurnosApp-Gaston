using Microsoft.EntityFrameworkCore;
using TurnosApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de la Base de Datos
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TurnosDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. AGREGAR LOS SERVICIOS NECESARIOS (CORS y Controladores)
builder.Services.AddControllers(); // Importante para que funcionen los endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(); // Habilita el servicio de CORS

var app = builder.Build();

// 3. CONFIGURACIÓN DEL PIPELINE (EL ORDEN ES CLAVE)

// Swagger siempre activo para Railway
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TurnosApp API V1");
    c.RoutePrefix = string.Empty;
});

// POLÍTICA DE CORS: Debe ir ANTES de MapControllers
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();
app.MapControllers();

// 4. Asegurar creación de base de datos
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TurnosDbContext>();
    context.Database.EnsureCreated();
}

// 5. Configuración del Puerto para Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
Console.WriteLine($"🚀 API escuchando en el puerto: {port}");

app.Run($"http://0.0.0.0:{port}");