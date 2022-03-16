using AbpLab.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AbpLab;

[DependsOn(
    typeof(AbpLabEntityFrameworkCoreTestModule)
    )]
public class AbpLabDomainTestModule : AbpModule
{

}
