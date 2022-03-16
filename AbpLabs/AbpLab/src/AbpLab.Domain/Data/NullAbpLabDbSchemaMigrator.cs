using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpLab.Data;

/* This is used if database provider does't define
 * IAbpLabDbSchemaMigrator implementation.
 */
public class NullAbpLabDbSchemaMigrator : IAbpLabDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
