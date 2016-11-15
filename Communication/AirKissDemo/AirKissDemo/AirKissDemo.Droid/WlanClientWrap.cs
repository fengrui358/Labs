using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AirKissDemo.Core;
using AirKissDemo.Droid;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Debug = System.Diagnostics.Debug;

[assembly: Dependency(typeof(WlanClientWrap))]
namespace AirKissDemo.Droid
{
    public class WlanClientWrap : IWlanClient
    {
        private ConnectivityManager _connectivityManager;
        private WifiManager _wifiManager;

        internal void InjectManager(ConnectivityManager connectivityManager, WifiManager wifiManager)
        {
            _connectivityManager = connectivityManager;
            _wifiManager = wifiManager;
        }

        public string GetCurrentWifiSSID()
        {
            var ssid = string.Empty;

            if (_connectivityManager != null && _wifiManager != null)
            {
                NetworkInfo networkInfo = _connectivityManager.ActiveNetworkInfo;

                if (networkInfo?.IsConnected ?? false)
                {
                    WifiInfo connectionInfo = _wifiManager.ConnectionInfo;
                    if (connectionInfo != null)
                    {
                        ssid = connectionInfo.SSID;
                        Debug.WriteLine("获取到当前的SSID：" + ssid);

                        if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1 && ssid.StartsWith("\"") &&
                            ssid.EndsWith("\""))
                        {
                            ssid = Regex.Replace(ssid, @"^\\|\\$", "");
                        }
                    }
                }
            }

            return ssid;
        }
    }
}