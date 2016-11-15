using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemConfiguration;
using AirKissDemo.Core;
using AirKissDemo.iOS;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(WlanClientWrap))]
namespace AirKissDemo.iOS
{
    public class WlanClientWrap : IWlanClient
    {
        public string GetCurrentWifiSSID()
        {
            NSDictionary nsDictionary;
            CaptiveNetwork.TryCopyCurrentNetworkInfo("en0", out nsDictionary);

            if (nsDictionary?[CaptiveNetwork.NetworkInfoKeySSID] == null)
            {
                return string.Empty;
            }

            return nsDictionary[CaptiveNetwork.NetworkInfoKeySSID].ToString();
        }
    }
}