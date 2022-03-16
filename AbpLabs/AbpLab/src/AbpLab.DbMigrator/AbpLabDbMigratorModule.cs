using AbpLab.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace AbpLab.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpLabEntityFrameworkCoreModule),
    typeof(AbpLabApplicationContractsModule)
    )]
public class AbpLabDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
