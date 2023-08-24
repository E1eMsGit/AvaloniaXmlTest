using AvaloniaXmlTest.Models;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;

namespace AvaloniaXmlTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DeviceModel _device;

        public MainWindowViewModel()
        {
            _device = new DeviceModel();
        }

        public ReactiveCommand<Unit, Unit> OnWindowLoaded => ReactiveCommand.Create(() =>
        {
            Debug.WriteLine("Loaded");
            MessageBus.Current.SendMessage(_device);
        });

        public ReactiveCommand<Unit, Unit> OnWindowClosing => ReactiveCommand.Create(() => Debug.WriteLine("Closing"));
    }
}
