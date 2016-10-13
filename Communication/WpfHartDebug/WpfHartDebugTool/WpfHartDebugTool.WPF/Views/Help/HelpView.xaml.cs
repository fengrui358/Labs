using WpfHartDebugTool.Core.ViewModels.Help;
using WpfHartDebugTool.WPF.Views.Base;

namespace WpfHartDebugTool.WPF.Views.Help
{
    [MvxRegion("PageContent")]
    public partial class HelpView : BaseView
    {
        public new HelpAndFeedbackViewModel ViewModel
        {
            get { return (HelpAndFeedbackViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public HelpView()
        {
            InitializeComponent();
        }
    }
}
