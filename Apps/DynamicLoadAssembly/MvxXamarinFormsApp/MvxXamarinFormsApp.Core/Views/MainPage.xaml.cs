using System;
using MvxXamarinFormsApp.Core.ViewModels;
using Xamarin.Forms;

namespace MvxXamarinFormsApp.Core.Views
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel ViewModel
        {
            get
            {
                return BindingContext as MainViewModel;
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //todo:避免重复注册
            ViewModel.LoadViewAction = LoadViewAction;
        }

        private void LoadViewAction(View view)
        {
            TestContainer.Children.Add(view);
        }
    }
}
