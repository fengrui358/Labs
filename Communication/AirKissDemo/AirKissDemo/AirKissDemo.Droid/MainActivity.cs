using System;
using AirKissDemo.Core;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.Net.Wifi;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Debug = System.Diagnostics.Debug;

namespace AirKissDemo.Droid
{
    [Activity(Label = "AirKissDemo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            BuildWlanClientWrap();
            LoadApplication(new App());

            BuildWlanClientWrap();
        }

        /// <summary>
        /// 向IOC容器中的WlanClientWrap填充一些必要的东西
        /// </summary>
        private void BuildWlanClientWrap()
        {
            var wlanClient = DependencyService.Get<IWlanClient>() as WlanClientWrap;
            wlanClient?.InjectManager(
                (ConnectivityManager) ApplicationContext.GetSystemService(ConnectivityService),
                (WifiManager) ApplicationContext.GetSystemService(WifiService));
        }
    }
}

