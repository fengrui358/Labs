using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace DataBasePerformanceLab.Data;

/* This is used if database provider does't define
 * IDataBasePerformanceLabDbSchemaMigrator implementation.
 */
public class NullDataBasePerformanceLabDbSchemaMigrator : IDataBasePerformanceLabDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
