using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataBasePerformanceLab.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class DataBasePerformanceLabDbContextFactory : IDesignTimeDbContextFactory<DataBasePerformanceLabDbContext>
{
    public DataBasePerformanceLabDbContext CreateDbContext(string[] args)
    {
        DataBasePerformanceLabEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<DataBasePerformanceLabDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default"));

        return new DataBasePerformanceLabDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../DataBasePerformanceLab.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
