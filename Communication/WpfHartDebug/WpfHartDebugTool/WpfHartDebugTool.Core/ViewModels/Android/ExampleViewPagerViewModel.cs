using WpfHartDebugTool.Core.ViewModels.Base;

namespace WpfHartDebugTool.Core.ViewModels.Android
{
	public class ExampleViewPagerViewModel : BaseViewModel
    {
        public RecyclerViewModel Recycler { get; private set; }

        public ExampleViewPagerViewModel()
        {
            Recycler = new RecyclerViewModel();
        }
    }
}