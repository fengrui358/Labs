using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Bluetooth.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InTheHand.Net.Sockets;

namespace WpfBlueToothLab
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Device> _slaveDevices;

        public ObservableCollection<Device> SlaveDevices
        {
            get { return _slaveDevices; }
            set { Set(() => SlaveDevices, ref _slaveDevices, value); }
        }

        public RelayCommand DiscoverCommand { get; private set; }

        public MainWindowViewModel()
        {
            SlaveDevices = new ObservableCollection<Device>();
            DiscoverCommand = new RelayCommand(Discover);
        }

        private async void Discover()
        {
            try
            {
                var devices = await ServiceLocal.SenderBluetoothService.GetDevices();

                foreach (var device in devices)
                {
                    SlaveDevices.Add(device);
                }
            }
            catch (PlatformNotSupportedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
