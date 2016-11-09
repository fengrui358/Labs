using WpfHartDebugTool.Core.ViewModels.Menu;
using WpfHartDebugTool.WPF.Views.Base;

namespace WpfHartDebugTool.WPF.Views.Menu
{
    [MvxRegion("MenuContent")]
    public partial class MenuView : BaseView
    {
        public new MenuViewModel ViewModel
        {
            get { return (MenuViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MenuView()
        {
            InitializeComponent();
        }
    }
}
