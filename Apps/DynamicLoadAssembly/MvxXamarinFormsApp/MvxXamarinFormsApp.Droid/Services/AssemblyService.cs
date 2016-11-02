using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using MvvmCross.Platform;
using MvxXamarinFormsApp.Core.Services;
using PluginBase;
using View = Xamarin.Forms.View;

namespace MvxXamarinFormsApp.Droid.Services
{
    public class AssemblyService : IAssemblyService
    {
        private Dictionary<string, IPlugin> _plugins = new Dictionary<string, IPlugin>();

        public void LoadFrom(string dllPath)
        {
            var assembly = Assembly.LoadFrom(dllPath);

            //todo:ɸѡʧ�ܣ�
            //var types = assembly.GetTypes().Where(s => s.IsAssignableFrom(typeof(IPlugin)));
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var plugin = Activator.CreateInstance(type) as IPlugin;

                if (!_plugins.ContainsKey(plugin.Name))
                {
                    _plugins.Add(plugin.Name, plugin);
                }
            }
        }

        public View GetView(string name)
        {
            if (_plugins.ContainsKey(name))
            {
                return _plugins[name].GetView();
            }

            return null;
        }
    }
}