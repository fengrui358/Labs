using System;
using System.Collections.Generic;
using System.Text;
using DataBasePerformanceLab.Localization;
using Volo.Abp.Application.Services;

namespace DataBasePerformanceLab;

/* Inherit your application services from this class.
 */
public abstract class DataBasePerformanceLabAppService : ApplicationService
{
    protected DataBasePerformanceLabAppService()
    {
        LocalizationResource = typeof(DataBasePerformanceLabResource);
    }
}
