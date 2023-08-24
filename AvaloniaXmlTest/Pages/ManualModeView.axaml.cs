using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaXmlTest.ViewModels;

namespace AvaloniaXmlTest.Pages
{
    public partial class ManualModeView : UserControl
    {
        public ManualModeView()
        {
            InitializeComponent();
            DataContext = new ManualModeViewModel();
        }
    }
}
