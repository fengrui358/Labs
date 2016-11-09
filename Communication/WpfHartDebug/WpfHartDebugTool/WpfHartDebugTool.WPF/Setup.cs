using System.Text;
using System.Windows.Threading;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Platform;
using MvvmCross.Wpf.Views;
using SerialPortChannel.Services;
using WpfHartDebugTool.Core.Interfaces;
using WpfHartDebugTool.WPF.Services;
using WpfHartDebugTool.WPF.Views;
using SerialPortChannel = SerialPortChannel.SerialPortChannel;

namespace WpfHartDebugTool.WPF
{
    public class Setup : MvxWpfSetup
    {
        public Setup(Dispatcher uiThreadDispatcher, IMvxWpfViewPresenter presenter) 
            : base(uiThreadDispatcher, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<IDialogService>(() => new DialogService());
            Mvx.RegisterSingleton<MvxPresentationHint>(() => new MvxPanelPopToRootPresentationHint());

            Mvx.RegisterType<ISerialPortChannel, global::SerialPortChannel.SerialPortChannel>();  
            Mvx.RegisterSingleton<ISerialPortService>(()=>new SerialPortService());          
        }
    }
}
