using System.Collections.Generic;
using AisOS.ViewModels;
using AisOS.Views.Login;
using AisOsDataBase.DataBaseRoot.Configuration;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MongoDB.Driver;

namespace AisOS.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var vm = new MainWindowViewModel();
            DataContext = vm;

            Opened += MainWindow_Opened;
        }

        private void MainWindow_Opened(object sender, System.EventArgs e)
        {
            LoginDialog loginDialog = new LoginDialog();
            loginDialog.ShowDialog(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
