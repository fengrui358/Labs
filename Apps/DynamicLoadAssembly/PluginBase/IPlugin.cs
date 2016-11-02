using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PluginBase
{
    public interface IPlugin
    {
        string Name { get; }

        View GetView();
    }
}
