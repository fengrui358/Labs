using System;
using System.Collections.Generic;
using System.Text;
using AbpLab.Localization;
using Volo.Abp.Application.Services;

namespace AbpLab;

/* Inherit your application services from this class.
 */
public abstract class AbpLabAppService : ApplicationService
{
    protected AbpLabAppService()
    {
        LocalizationResource = typeof(AbpLabResource);
    }
}
