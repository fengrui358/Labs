﻿using WpfHartDebugTool.Core.ViewModels.Login;
using WpfHartDebugTool.WPF.Views.Base;

namespace WpfHartDebugTool.WPF.Views.Login
{
    public partial class LoginView : BaseView
    {
        public new LoginViewModel ViewModel
        {
            get { return (LoginViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public LoginView()
        {
            InitializeComponent();
        }
    }
}
