using AvaloniaXmlTest.Domains;
using AvaloniaXmlTest.Interfaces;
using AvaloniaXmlTest.Models;
using ReactiveUI;
using System.Reactive;

namespace AvaloniaXmlTest.ViewModels
{
    public class ManualModeViewModel : ViewModelBase
    {
        public DeviceModel Device
        {
            get => _device;
            set => this.RaiseAndSetIfChanged(ref _device, value);
        }
        public string LogText => Log.GetInstance().Print(Log.Modes.Manual);
        public int TextBoxCaretIndex
        {
            get
            {
                if (_device == null)
                    return 0;
                else
                    return LogText.Length;
            }
        }
        public bool IsTimeoutChecked
        {
            get => _isTimeoutChecked;
            set => this.RaiseAndSetIfChanged(ref _isTimeoutChecked, value);
        }

        public int Tms
        {
            get => _tms;
            set => this.RaiseAndSetIfChanged(ref _tms, value);
        }

        public ReactiveCommand<IRelay, Unit> SendCommand =>
            ReactiveCommand.Create<IRelay>((relay) =>
            {
                _device?.SendCommand(relay, IsTimeoutChecked, Tms);
                Properties.Settings.Default.ManualModeTms = Tms;
                Properties.Settings.Default.Save();
            });

        private DeviceModel? _device;
        private bool _isTimeoutChecked;
        private int _tms;

        public ManualModeViewModel()
        {
            _device = null;

            MessageBus.Current.Listen<DeviceModel>().Subscribe(Observer.Create<DeviceModel>((device) =>
            {
                Device = device;
                Device.CommandCompleted += delegate (string answer)
                {
                    Log.GetInstance().Write(Log.Modes.Manual, answer);
                    this.RaisePropertyChanged(nameof(LogText));
                    this.RaisePropertyChanged(nameof(TextBoxCaretIndex));
                };
            }));

            Tms = Properties.Settings.Default.ManualModeTms;
        }
    }
}
