using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirKissDemo.Core
{
    public interface IWlanClient
    {
        /// <summary>
        /// 获取当前wifi的ssid
        /// </summary>
        /// <returns></returns>
        string GetCurrentWifiSSID();
    }
}
