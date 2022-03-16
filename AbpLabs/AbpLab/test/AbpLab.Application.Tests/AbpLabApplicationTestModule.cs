using Volo.Abp.Modularity;

namespace AbpLab;

[DependsOn(
    typeof(AbpLabApplicationModule),
    typeof(AbpLabDomainTestModule)
    )]
public class AbpLabApplicationTestModule : AbpModule
{

}
