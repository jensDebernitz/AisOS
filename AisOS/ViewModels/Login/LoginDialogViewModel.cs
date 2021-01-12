using System;
using System.Reactive;
using AisOsDataBase.DataBaseRoot.Configuration;
using AisOsDataBase.DataBaseRoot.General;
using AisOsUtils.Password;
using Avalonia.Controls;
using Avalonia.Media;
using MongoDB.Driver;
using ReactiveUI;

namespace AisOS.ViewModels.Login
{
    public class LoginDialogViewModel : ViewModelBase
    {
        private string _username = "";
        private string _password = "";
        private bool _loginButtonEnable = false;
        public ReactiveCommand<Window, Unit> Login { get; }
        private SolidColorBrush _passwordBorderBrush = new SolidColorBrush(new Color(0xFF, 0x44, 0x44, 0x44));

        public LoginDialogViewModel()
        {
            LoadLastUsername();

            UserAccounts userAccounts = new UserAccounts();
            UserAccount userAccount = userAccounts.GetCollection()
                .Find(account => account.Credential.UserName == Username).ToList()[0];

            var language = "en-US";

            if (userAccount != null)
            {
                language = userAccount.Language;
            }

            Localizer.Localizer.Instance.LoadLanguage(language);
            Login = ReactiveCommand.Create<Window>(RunLogin);
        }

        private void LoadLastUsername()
        {
            Configurations configurations = new Configurations();
            Configuration configuration = configurations.GetCollection().Find(_ => true).ToList()[0];

            if (configurations.CollectionExists() == true && configuration.LastUserLogin != null)
            {
                Username = configuration.LastUserLogin.Credential.UserName;
            }
        }

        private void RunLogin(Window window)
        {
            bool passwordValid = false;

            if (Username.ToLower().Equals("superuser"))
            {
                string passwordOfTheDay = PasswordGenerator.GeneratePassword(DateTime.Now);
                
                if (Password.Equals(passwordOfTheDay) == true)
                {
                    window?.Close();
                    passwordValid = true;
                }
            }
            else
            {
                UserAccounts userAccounts = new UserAccounts();
                UserAccount userAccount = userAccounts.GetCollection()
                    .Find(account => account.Credential.UserName == _username).ToList()[0];

                if (Password.Equals(userAccount.Credential.Password) == true)
                {
                    Configurations configurations = new Configurations();
                    Configuration configuration = null;

                    if (configurations.CollectionExists() == false)
                    {
                        configuration = new Configuration {LastUserLogin = userAccount};
                        configurations.GetCollection().InsertOne(configuration);
                    }
                    else
                    {
                        configuration = configurations.GetCollection().Find(_ => true).ToList()[0];
                        configuration.LastUserLogin = userAccount;

                        var query = new QueryDocument("Id", configuration.Id); //this is the filter
                        var result = configurations.GetCollection().ReplaceOneAsync(doc => doc.Id == configuration.Id,
                            configuration,
                            new ReplaceOptions {IsUpsert = true});
                    }

                    passwordValid = true;
                    window?.Close();
                }
            }

            if (passwordValid == false)
            {
                PasswordBorderBrush = new SolidColorBrush(new Color(0xff, 0xcc, 0x00, 0x00));
            }
        }
        
        private void CheckUserName()
        {
            string username = _username;

            if (username.ToLower().Equals("superuser"))
            {
                LoginButtonEnable = true;
            }
            else
            {
                UserAccounts userAccounts = new UserAccounts();

                LoginButtonEnable = userAccounts.GetCollection()
                    .Find(account => account.Credential.UserName == username)
                    .ToList().Count == 1;
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                this.RaiseAndSetIfChanged(ref _username, value);
                CheckUserName();
            }
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
            
        }

        public bool LoginButtonEnable
        {
            get => _loginButtonEnable; 
            set => this.RaiseAndSetIfChanged(ref _loginButtonEnable, value);
        }

        public SolidColorBrush PasswordBorderBrush
        {
            get => _passwordBorderBrush;
            set => this.RaiseAndSetIfChanged(ref _passwordBorderBrush, value);
        }
    }
}
