using System.Windows.Controls;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using MvvmCross.Wpf.Views;

namespace WpfHartDebugTool.WPF.Views
{
    public class MvxWpfPage : Page, IMvxWpfView, IMvxView, IMvxDataConsumer
    {
        private IMvxViewModel _viewModel;

        public IMvxViewModel ViewModel
        {
            get { return this._viewModel; }
            set
            {
                this._viewModel = value;
                this.DataContext = value;
            }
        }
    }

    public class MvxWpfPage<TViewModel>
        : MvxWpfPage, IMvxWpfView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value;  }
        }
    }


}
