using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TurnosApp.Infrastructure.Data
{
    // Clase necesaria para que 'dotnet ef' pueda construir el DbContext en tiempo de diseño
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TurnosDbContext>
    {
        public TurnosDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // Si la configuracion está en el proyecto API, ajusta la ruta
                .AddJsonFile("appsettings.json", optional: false) 
                .Build();

            var builder = new DbContextOptionsBuilder<TurnosDbContext>();

            // ** LA CORRECCIÓN CLAVE: USAR UseSqlite EN LUGAR DE UseNpgsql **
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlite(connectionString);

            return new TurnosDbContext(builder.Options);
        }
    }
}