using AvaloniaXmlTest.Interfaces;

namespace AvaloniaXmlTest.Domains
{
    public class Command
    {
        public string GetCommand { get; }

        public Command(IRelay relay)
        {
            GetCommand = relay.Command;
        }

        public Command(IRelay relay, bool isTimeoutChecked, int timeout)
        {
            if (!isTimeoutChecked)
            {
                GetCommand = relay.Command;
            }
            else
            {
                GetCommand = $"{relay.Command}Таймаут: {timeout} мс\n";
            }
        }
    }
}
