using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace AbpLab.Web;

[Dependency(ReplaceServices = true)]
public class AbpLabBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AbpLab";
}
