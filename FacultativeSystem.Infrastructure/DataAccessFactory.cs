// FacultativeSystem.Infrastructure/DataAccessFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FacultativeSystem.Infrastructure;

public class DataAccessFactory : IDesignTimeDbContextFactory<DataAccess>
{
    public DataAccess CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataAccess>();

        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "FacultativeSystem.Api");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("WebDatabase");
        optionsBuilder.UseNpgsql(connectionString);

        return new DataAccess(optionsBuilder.Options);
    }
}