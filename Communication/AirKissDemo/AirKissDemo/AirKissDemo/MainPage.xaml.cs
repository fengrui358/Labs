using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirKissDemo.Core;
using Xamarin.Forms;

namespace AirKissDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(DependencyService.Get<IUdpServer>(),
                DependencyService.Get<IUdpClient>());
        }
    }
}
