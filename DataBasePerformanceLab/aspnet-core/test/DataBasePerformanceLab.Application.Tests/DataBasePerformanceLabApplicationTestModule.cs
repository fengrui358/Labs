using Volo.Abp.Modularity;

namespace DataBasePerformanceLab;

[DependsOn(
    typeof(DataBasePerformanceLabApplicationModule),
    typeof(DataBasePerformanceLabDomainTestModule)
    )]
public class DataBasePerformanceLabApplicationTestModule : AbpModule
{

}
