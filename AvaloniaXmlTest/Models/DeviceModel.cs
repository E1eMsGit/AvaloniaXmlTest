using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using AvaloniaXmlTest.Domains;
using AvaloniaXmlTest.Interfaces;

namespace AvaloniaXmlTest.Models
{
    public class DeviceModel
    {
        private PultModel _a1;
        private PultModel _a2;
        private PultModel _a3;

        public List<PultModel> DeviceParts { get; set; }

        public event Action TestStart;
        public event Action TestCompleted;
        public event Action<string> CommandCompleted;
        public event Action<string> TestCommandCompleted;
        public event Action<int> UpdateTestProgress;

        public DeviceModel()
        {
            try
            {
                List<RelayModel> _relaysList = XmlParser.GetRelays("matrix.xml");
                _a1 = new PultModel(0, _relaysList);
                _a2 = new PultModel(44, _relaysList);
                _a3 = new PultModel(88, _relaysList);

                DeviceParts = new List<PultModel> { _a1, _a2, _a3 };
            }
            catch (Exception)
            {

                Debug.WriteLine("matrix.xml not found");
            }
        }

        public async Task SendCommand(IRelay relay, bool isTimeoutChecked, int timeout)
        {
            if (isTimeoutChecked)
            {
                System.Timers.Timer _timer = new() { Interval = timeout };
                _timer.Elapsed += delegate { _timer?.Stop(); relay.IsEnabled = false; };
                _timer.Start();
            }

            await Task.Run(() =>
            {
                CommandCompleted?.Invoke(new Command(relay, isTimeoutChecked, timeout).GetCommand);
            });
        }

        public async Task StartTestFromFile(CancellationToken token, List<RelayModel> _relaysList)
        {
            await Task.Run(() =>
            {
                TestStart?.Invoke();

                for (int i = 0; i < _relaysList.Count; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    TestCommandCompleted?.Invoke(new Command(_relaysList[i]).GetCommand);
                    Thread.Sleep(100);
                }

                TestCompleted?.Invoke();
            });
        }
    }
}
