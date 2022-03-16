using AbpLab.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace AbpLab.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class AbpLabPageModel : AbpPageModel
{
    protected AbpLabPageModel()
    {
        LocalizationResourceType = typeof(AbpLabResource);
    }
}
