using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace PharmacyAPI.Data
{
    public class PharmacyDbContextFactory : IDesignTimeDbContextFactory<PharmacyDbContext>
    {
        public PharmacyDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PharmacyDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new PharmacyDbContext(optionsBuilder.Options);
        }
    }
}
