using AisOS.ViewModels.Login;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AisOS.Views.Login
{
    public class LoginDialog : Window
    {
        public LoginDialog()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            DataContext = new LoginDialogViewModel();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            LoginConfigurations loginConfigurations = new LoginConfigurations();
             ((LoginConfigurationsViewModel) loginConfigurations.DataContext).Username = ((LoginDialogViewModel)DataContext).Username;
            loginConfigurations.ShowDialog(this);
        }
    }
}
