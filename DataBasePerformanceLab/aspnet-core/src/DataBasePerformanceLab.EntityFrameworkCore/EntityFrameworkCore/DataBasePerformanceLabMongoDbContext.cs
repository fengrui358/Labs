using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DataBasePerformanceLab.EntityFrameworkCore
{
    [ConnectionStringName("MongodbConnectionString")]
    public class DataBasePerformanceLabMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<DeviceGps.DeviceGps> DeviceGps => Collection<DeviceGps.DeviceGps>();
    }
}
