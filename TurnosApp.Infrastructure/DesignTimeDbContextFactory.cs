using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; // <--- ESENCIAL
using Microsoft.Extensions.Configuration.Json; // <--- ESENCIAL
using System.IO;
using TurnosApp.Infrastructure.Data;

namespace TurnosApp.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TurnosDbContext>
    {
        public TurnosDbContext CreateDbContext(string[] args)
        {
            // 1. Configurar para leer el appsettings.json de la API
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TurnosApp.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TurnosDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 2. CAMBIO CRUCIAL: Usar Npgsql (PostgreSQL) en lugar de UseSqlite
            builder.UseNpgsql(connectionString);

            return new TurnosDbContext(builder.Options);
        }
    }
}