namespace AvaloniaXmlTest.Interfaces
{
    public interface IRelay
    {
        public string Name { get; }
        public int Pult { get; }
        public int Module { get; }
        public int Position { get; }
        public bool IsEnabled { get; set; }
        public string Command { get; set; }
    }
}
