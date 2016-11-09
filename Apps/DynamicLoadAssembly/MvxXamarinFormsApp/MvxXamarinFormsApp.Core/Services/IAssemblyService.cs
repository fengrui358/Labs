using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MvxXamarinFormsApp.Core.Services
{
    public interface IAssemblyService
    {
        void LoadFrom(string dllPath);

        View GetView(string name);
    }
}
