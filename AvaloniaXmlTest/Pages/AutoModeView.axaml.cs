using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaXmlTest.ViewModels;

namespace AvaloniaXmlTest.Pages
{
    public partial class AutoModeView : UserControl
    {
        public AutoModeView()
        {
            InitializeComponent();
            DataContext = new AutoModeViewModel();
        }
    }
}
