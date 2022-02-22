using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataBasePerformanceLab.Data;
using Volo.Abp.DependencyInjection;

namespace DataBasePerformanceLab.EntityFrameworkCore;

public class EntityFrameworkCoreDataBasePerformanceLabDbSchemaMigrator
    : IDataBasePerformanceLabDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreDataBasePerformanceLabDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the DataBasePerformanceLabDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<DataBasePerformanceLabDbContext>()
            .Database
            .MigrateAsync();
    }
}
