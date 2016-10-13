﻿using System.Windows;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using WpfHartDebugTool.WPF.Views;

namespace WpfHartDebugTool.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        bool _setupComplete;

        void DoSetup()
        {
            var presenter = new CustomViewPresenter(MainWindow);

            var setup = new Setup(Dispatcher, presenter);
            setup.Initialize();

            var start = Mvx.Resolve<IMvxAppStart>();
            start.Start();

            _setupComplete = true;
        }

        protected override void OnActivated(System.EventArgs e)
        {
            if (!_setupComplete)
                DoSetup();

            base.OnActivated(e);
        }
    }
}
