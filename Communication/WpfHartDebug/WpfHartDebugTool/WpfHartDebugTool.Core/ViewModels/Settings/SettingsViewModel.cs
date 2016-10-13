using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using WpfHartDebugTool.Core.Interfaces;
using WpfHartDebugTool.Core.ViewModels.Base;

namespace WpfHartDebugTool.Core.ViewModels.Settings
{
	public class SettingsViewModel : BaseViewModel
    {
        private string _port = "COM6";
        private bool _openSerialStatus;
	    private ISerialPortChannel _currentSerialPortChannel;
	    private ISerialPortService _serialPortService;

        public string Port
	    {
	        get { return _port; }
	        set { SetProperty(ref _port, value); }
	    }

        public bool OpenSerialStatus => _currentSerialPortChannel?.IsOpen ?? false;

	    public MvxCommand OpenSerialPortCommand => new MvxCommand(OpenSerialPortHandler);

	    public SettingsViewModel(ISerialPortService serialPortService)
	    {
	        _serialPortService = serialPortService;
	    }

	    public override void Start()
	    {
	        var s = _serialPortService.GetPortNames();
	    }

	    private void OpenSerialPortHandler()
	    {
	        _currentSerialPortChannel = Mvx.Resolve<ISerialPortChannel>();
            _currentSerialPortChannel.Open(_port, 1200);

            _currentSerialPortChannel.DataReceived += CurrentSerialPortChannelOnDataReceived;

	        Task.Delay(1000).Wait();

	        var sendBytes = new byte[] {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x02, 0x80, 0x00, 0x00, 0x82};

            _currentSerialPortChannel.WritePort(sendBytes);
	    }

	    private void CurrentSerialPortChannelOnDataReceived(object sender, byte[] bytes)
	    {
            var s = new StringBuilder();
	        foreach (var b in bytes)
	        {
	            s.Append(string.Concat(b.ToString("X"), " "));
	        }

            Mvx.Trace(s.ToString());
	    }
    }
}