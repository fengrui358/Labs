using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DataBasePerformanceLab.DeviceGps
{
    public class DeviceGpsService : DataBasePerformanceLabAppService
    {
        private static readonly DateTime _startTime = new(2022, 2, 2, 2, 2, 2, DateTimeKind.Utc);
        private static readonly Random _random = new();
        private readonly IRepository<DeviceGps, Guid> _repository;
        private readonly TimeSpan _batchDuration = TimeSpan.FromMinutes(5);

        public DeviceGpsService(IRepository<DeviceGps, Guid> repository)
        {
            _repository = repository;
        }


        public async Task<string> InitData()
        {
            var batchDuration = _batchDuration;
            // 读取 guid
            var uuids = await File.ReadAllLinesAsync("uuid.txt");
            var datas = new List<DeviceGps>();
            var latest = new Dictionary<string, DeviceGps>();

            var startTime = _startTime;
            if (await _repository.AnyAsync())
            {
                startTime = await _repository.MaxAsync(s => s.CreationTime);
            }

            while (batchDuration >= TimeSpan.Zero)
            {
                foreach (var uuid in uuids)
                {
                    var imei = uuid;
                    var gps = GetRandomGps(latest.ContainsKey(imei) ? latest[imei] : null);
                    gps.CreationTime = _startTime;
                    gps.Imei = imei;

                    datas.Add(gps);
                    latest[imei] = gps;
                }

                startTime = startTime.AddSeconds(5);
                batchDuration = batchDuration.Subtract(TimeSpan.FromSeconds(5));
            }

            var sw = Stopwatch.StartNew();

            await _repository.InsertManyAsync(datas);

            sw.Stop();
            return $"ElapsedMilliseconds: {sw.ElapsedMilliseconds}";
        }

        private DeviceGps GetRandomGps(DeviceGps latestDeviceGps)
        {
            var lat = latestDeviceGps?.Latitude ?? 30.663429;
            var lon = latestDeviceGps?.Longitude ?? 104.08824;
            var alt = latestDeviceGps?.Altitude ?? 458.4434700589627;

            var r = _random.Next() * 0.01;
            lat += (_random.Next() > 0.5 ? 1 * r : -1 * r);
            lon += (_random.Next() > 0.5 ? 1 * r : -1 * r);
            alt += (_random.Next() > 0.5 ? 1 * r : -1 * r);

            return new DeviceGps
            {
                Latitude = lat,
                Longitude = lon,
                Altitude = alt
            };
        }
    }
}
