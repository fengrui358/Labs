using AbpLab.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpLab.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AbpLabController : AbpControllerBase
{
    protected AbpLabController()
    {
        LocalizationResource = typeof(AbpLabResource);
    }
}
