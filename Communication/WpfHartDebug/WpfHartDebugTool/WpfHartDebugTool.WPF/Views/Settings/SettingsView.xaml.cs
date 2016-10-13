using WpfHartDebugTool.Core.ViewModels.Settings;
using WpfHartDebugTool.WPF.Views.Base;

namespace WpfHartDebugTool.WPF.Views.Settings
{
    [MvxRegion("PageContent")]
    public partial class SettingsView : BaseView
    {
        public new SettingsViewModel ViewModel
        {
            get { return (SettingsViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public SettingsView()
        {
            InitializeComponent();
        }
    }
}
