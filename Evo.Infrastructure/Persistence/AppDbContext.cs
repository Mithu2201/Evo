using Evo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Evo.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<ServiceProvider> ServiceProviders => Set<ServiceProvider>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //dotnet new tool-manifest
        //dotnet tool install dotnet-ef

        //dotnet ef migrations add InitialCreate --project Evo.Infrastructure --startup-project Evo.API --output-dir Persistence/Migrations

        //dotnet ef database update --project Evo.Infrastructure --startup-project Evo.API
    }
}
