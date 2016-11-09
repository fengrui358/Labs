using WpfHartDebugTool.Core.ViewModels.Home;
using WpfHartDebugTool.WPF.Views.Base;

namespace WpfHartDebugTool.WPF.Views.Home
{
    [MvxRegion("PageContent")]
    public partial class ThirdView : BaseView
    {
        public new ThirdViewModel ViewModel
        {
            get { return (ThirdViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public ThirdView()
        {
            InitializeComponent();
        }
    }
}
