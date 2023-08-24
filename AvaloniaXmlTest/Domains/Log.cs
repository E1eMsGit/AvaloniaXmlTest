using System.Text;

namespace AvaloniaXmlTest.Domains
{
    public class Log
    {
        private StringBuilder _manualModeLogText;
        private StringBuilder _autoModeLogText;
        private static Log _instance;

        public enum Modes
        {
            Manual,
            Auto
        }

        private Log()
        {
            _manualModeLogText = new StringBuilder();
            _autoModeLogText = new StringBuilder();
        }

        public static Log GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Log();
            }

            return _instance;
        }

        public void Write(Modes mode, string message)
        {
            switch (mode)
            {
                case Modes.Manual:
                    _manualModeLogText.AppendLine(message);
                    break;
                case Modes.Auto:
                    _autoModeLogText.AppendLine(message);
                    break;
                default:
                    break;
            }
        }

        public string Print(Modes mode)
        {
            if (mode == Modes.Manual)
            {
                return _manualModeLogText.ToString();
            }
            else
            {
                return _autoModeLogText.ToString();
            }
        }
    }
}
