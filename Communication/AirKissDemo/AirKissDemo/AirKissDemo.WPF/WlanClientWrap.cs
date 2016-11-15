using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AirKissDemo.Core;
using NativeWifi;

namespace AirKissDemo.WPF
{
    public class WlanClientWrap :IWlanClient
    {
        private readonly Lazy<WlanClient> _wlanClient = new Lazy<WlanClient>(() => new WlanClient(),
            LazyThreadSafetyMode.PublicationOnly);

        public string GetCurrentWifiSSID()
        {
            var ssid = string.Empty;

            var wlanclient = _wlanClient.Value;
            if (wlanclient?.Interfaces != null)
            {
                var connector = wlanclient.Interfaces.FirstOrDefault(
                    s => s.CurrentConnection.isState == Wlan.WlanInterfaceState.Connected);

                ssid = connector?.CurrentConnection.profileName;
            }

            return ssid;
        }
    }
}
