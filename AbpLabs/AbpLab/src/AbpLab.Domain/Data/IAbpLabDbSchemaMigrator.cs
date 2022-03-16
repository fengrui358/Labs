using System.Threading.Tasks;

namespace AbpLab.Data;

public interface IAbpLabDbSchemaMigrator
{
    Task MigrateAsync();
}
