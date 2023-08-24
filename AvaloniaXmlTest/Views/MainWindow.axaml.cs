using System;
using Avalonia.Markup.Xaml;
using Avalonia.Controls;
using JetBrains.Annotations;


namespace AvaloniaXmlTest.Views
{
    public partial class MainWindow : Window
    {
        [CanBeNull]
        public event EventHandler<EventArgs> Loaded; // �������, �� ����� � ������� ������� Loaded �� Window.

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Loaded?.Invoke(this, new EventArgs());
        }
    }
}
