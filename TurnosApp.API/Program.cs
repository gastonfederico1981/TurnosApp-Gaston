using Microsoft.EntityFrameworkCore;
using TurnosApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la Base de Datos
builder.Services.AddDbContext<TurnosDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddCors(options => {
    options.AddPolicy("PermitirTodo", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Orden crítico de middlewares
app.UseCors("PermitirTodo");
app.MapControllers();

Console.WriteLine("-------------------------------------------");
Console.WriteLine("🚀 LA API ESTÁ PREPARADA EN EL PUERTO 5080");
Console.WriteLine("-------------------------------------------");

app.Run("http://localhost:5080");