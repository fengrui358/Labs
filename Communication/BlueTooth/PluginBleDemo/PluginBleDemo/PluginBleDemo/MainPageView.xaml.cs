﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PluginBleDemo
{
    public partial class MainPageView : ContentPage
    {
        public MainPageView()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }
    }
}
