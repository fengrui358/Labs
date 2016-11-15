using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirKissDemo.Core;
using NativeWifi;

namespace AirKissDemo.WPF
{
    public class WlanClientWrap :IWlanClient
    {
        private WlanClient _wlanClient;

        public WlanClientWrap()
        {
            
        }

        public string GetCurrentWifiSSID()
        {
            if (_wlanClient == null)
            {
                _wlanClient = new WlanClient();
            }

            var ssid = string.Empty;

            if (_wlanClient != null && _wlanClient.Interfaces != null)
            {
                var connector = _wlanClient.Interfaces.FirstOrDefault(
                    s => s.CurrentConnection.isState == Wlan.WlanInterfaceState.Connected);

                ssid = connector?.CurrentConnection.profileName;
            }

            return ssid;
        }
    }
}
