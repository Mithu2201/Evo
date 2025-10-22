using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Infrastructure.Persistence
{
    // Factory used by EF Core tools at design time (e.g., migrations)
    // Avoids needing runtime DI to construct DbContext.
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Get current directory of Infrastructure project
            var infraPath = Directory.GetCurrentDirectory();

            // Build path to appsettings.json in API project
            var apiPath = Path.Combine(infraPath, "..", "Evo.API", "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(apiPath, optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
