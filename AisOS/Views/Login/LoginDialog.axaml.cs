using AisOS.ViewModels.Login;
using Avalonia;
using Avalonia.Controls;
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
    }
}
