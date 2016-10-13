using WpfHartDebugTool.Core.ViewModels.Home;
using WpfHartDebugTool.WPF.Views.Base;

namespace WpfHartDebugTool.WPF.Views.Home
{
    [MvxRegion("PageContent")]
    public partial class HomeView : BasePage
    {
        public new HomeViewModel ViewModel
        {
            get { return (HomeViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public HomeView()
        {
            InitializeComponent();
        }
    }
}
