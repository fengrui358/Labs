using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace DataBasePerformanceLab;

[Dependency(ReplaceServices = true)]
public class DataBasePerformanceLabBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DataBasePerformanceLab";
}
