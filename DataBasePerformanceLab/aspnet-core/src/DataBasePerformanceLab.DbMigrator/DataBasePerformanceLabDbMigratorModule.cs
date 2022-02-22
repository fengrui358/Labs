using DataBasePerformanceLab.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace DataBasePerformanceLab.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DataBasePerformanceLabEntityFrameworkCoreModule),
    typeof(DataBasePerformanceLabApplicationContractsModule)
    )]
public class DataBasePerformanceLabDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
