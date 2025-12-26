using Microsoft.EntityFrameworkCore;
using TurnosApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de la Base de Datos con validación
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TurnosDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

// 2. AGREGAR ESTO: Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("PermitirTodo", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// 3. AGREGAR ESTO: Habilitar Swagger siempre en Railway para probar
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TurnosApp API V1");
    c.RoutePrefix = string.Empty; // Esto hace que Swagger cargue apenas abrís el link
});

app.UseCors("PermitirTodo");
app.MapControllers();

// 4. Crear la base de datos automáticamente si no existe (Opcional pero recomendado ahora)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TurnosDbContext>();
    context.Database.EnsureCreated();
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
Console.WriteLine($"🚀 API escuchando en el puerto: {port}");

app.Run($"http://0.0.0.0:{port}");