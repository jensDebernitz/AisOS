using AisOS.ViewModels.Login;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AisOS.Views.Login
{
    public class LoginConfigurations : Window
    {
        public LoginConfigurations()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var vm = new LoginConfigurationsViewModel();
            DataContext = vm;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
