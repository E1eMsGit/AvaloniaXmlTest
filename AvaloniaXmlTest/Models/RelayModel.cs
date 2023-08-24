using AvaloniaXmlTest.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AvaloniaXmlTest.Models
{
    public class RelayModel : IRelay, INotifyPropertyChanged
    {
        private bool _isEnabled;

        public string Name { get; }
        public int Pult { get; }
        public int Module { get; }
        public int Position { get; }
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
                Command = $"Название: {Name}\nПульт: {Pult}\nМодуль: {Module}\nПозиция: {Position}\nСостояние: {IsEnabled}\n";
            }
        }
        public string Command { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public RelayModel(string name, int pult, int module, int position)
        {
            Name = name;
            Pult = pult;
            Module = module;
            Position = position;
            Command = $"Название: {Name}\nПульт: {Pult}\nМодуль: {Module}\nПозиция: {Position}\nСостояние: {IsEnabled}\n";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
