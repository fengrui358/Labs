using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginBase;
using Xamarin.Forms;

namespace PluginTest
{
    public class Plugin : IPlugin
    {
        public string Name => "测试插件";

        public View GetView()
        {
            return new Entry {Text = "测试动态加载", TextColor = Color.Red, BackgroundColor = Color.Black};
        }
    }
}
