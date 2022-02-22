using DataBasePerformanceLab.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DataBasePerformanceLab.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class DataBasePerformanceLabController : AbpControllerBase
{
    protected DataBasePerformanceLabController()
    {
        LocalizationResource = typeof(DataBasePerformanceLabResource);
    }
}
