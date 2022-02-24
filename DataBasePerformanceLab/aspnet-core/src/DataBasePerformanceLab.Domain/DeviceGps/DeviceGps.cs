using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace DataBasePerformanceLab.DeviceGps
{
    public class DeviceGps : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 设备标识
        /// </summary>
        public string Imei { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public double? Altitude { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
