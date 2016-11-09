using WpfHartDebugTool.Core.ViewModels.Home;
using WpfHartDebugTool.WPF.Views.Base;

namespace WpfHartDebugTool.WPF.Views.Home
{
    [MvxRegion("PageContent")]
    public partial class InfoView : BaseView
    {
        public new InfoViewModel ViewModel
        {
            get { return (InfoViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
        public InfoView()
        {
            InitializeComponent();
        }
    }
}
