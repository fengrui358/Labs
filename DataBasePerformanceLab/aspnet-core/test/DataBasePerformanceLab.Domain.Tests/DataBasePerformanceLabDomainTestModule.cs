using DataBasePerformanceLab.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DataBasePerformanceLab;

[DependsOn(
    typeof(DataBasePerformanceLabEntityFrameworkCoreTestModule)
    )]
public class DataBasePerformanceLabDomainTestModule : AbpModule
{

}
