using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace PluginBleDemo
{
    public partial class MainPageView : ContentPage
    {
        private MainPageViewModel _viewModel;

        public MainPageView()
        {
            InitializeComponent();

            _viewModel = new MainPageViewModel(Navigation);
            BindingContext = _viewModel;
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {            
            _viewModel.ItemTap((e.Item as IDevice));
        }
    }
}
