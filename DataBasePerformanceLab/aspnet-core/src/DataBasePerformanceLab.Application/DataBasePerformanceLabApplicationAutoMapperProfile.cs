using AutoMapper;

namespace DataBasePerformanceLab;

public class DataBasePerformanceLabApplicationAutoMapperProfile : Profile
{
    public DataBasePerformanceLabApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<DeviceGps.DeviceGps, DeviceGpsDto.DeviceGpsDto>();
    }
}
