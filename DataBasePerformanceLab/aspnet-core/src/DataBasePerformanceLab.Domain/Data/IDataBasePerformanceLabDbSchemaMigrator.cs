using System.Threading.Tasks;

namespace DataBasePerformanceLab.Data;

public interface IDataBasePerformanceLabDbSchemaMigrator
{
    Task MigrateAsync();
}
