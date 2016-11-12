using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AirKissLib;

namespace AirKissDemo.WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private AirKissTask _airKissTask;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            var airKissEncoder = new AirKissEncoder(SsidText.Text, PswText.Text);

            _airKissTask = new AirKissTask(airKissEncoder);
            _airKissTask.Execute();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            _airKissTask?.Stop();
        }
    }
}
