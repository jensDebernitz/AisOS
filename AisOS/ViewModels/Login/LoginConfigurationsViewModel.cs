using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using AisOsDataBase.DataBaseRoot.General;
using Avalonia.Controls;
using MongoDB.Driver;
using ReactiveUI;

namespace AisOS.ViewModels.Login
{
    public class LoginConfigurationsViewModel : ViewModelBase
    {
        public List<string> I18NList { get; set; } = new List<string>(new string[] { "en-US", "de-DE" });
        public ReactiveCommand<Window, Unit> Close { get; }
        private int _selectedLanguageItem;
        public string Username { get; set; }

        public LoginConfigurationsViewModel()
        {
            Close = ReactiveCommand.Create<Window>(RunClose);
            SelectedLanguageItem = 0;
        }

        private void RunClose(Window window)
        {
            window?.Close();
        }

        public int SelectedLanguageItem
        {
            get => _selectedLanguageItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedLanguageItem, value);

                if (_selectedLanguageItem != -1)
                {
                    Localizer.Localizer.Instance.LoadLanguage(I18NList[_selectedLanguageItem]);

                    UserAccounts userAccounts = new UserAccounts();

                    if (userAccounts.GetCollection().Find(account => account.Credential.UserName == Username)
                        .ToList().Count != 0)
                    {
                        UserAccount userAccount = userAccounts.GetCollection()
                            .Find(account => account.Credential.UserName == Username).ToList()[0];

                        userAccount.Language = I18NList[_selectedLanguageItem];
                        var query = new QueryDocument("Id", userAccount.Id); //this is the filter
                        var result = userAccounts.GetCollection().ReplaceOneAsync(doc => doc.Id == userAccount.Id, userAccount, new ReplaceOptions { IsUpsert = true });

                    }
                }
            }
        }
    }
}
