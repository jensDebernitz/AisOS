using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;
using System;
using System.Security;
using AisOsDataBase;
using AisOsDataBase.DataBaseRoot.General;
using AisOsUtils.Password;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace AisOS
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            DataBaseConnection.Instance().Init();

            CreateAdminUser();
            
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        private static void CreateAdminUser()
        {
            UserAccounts userAccounts = new UserAccounts();

            if (userAccounts.GetCollection().Find(account => account.Credential.UserName == "admin")
                .ToList().Count == 0)
            {
                UserAccount adminAccount = new UserAccount
                {
                    Credential = new Credential {Password = "admin", UserName = "admin"},
                    Person = new Person {FirstName = "Jens", LastName = "Debernitz"}
                };

                userAccounts.GetCollection().InsertOne(adminAccount);
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug()
                .UseReactiveUI();

        private static SecureString SecureStringConverter(string pass)
        {
            SecureString ret = new SecureString();

            foreach (char chr in pass.ToCharArray())
                ret.AppendChar(chr);

            return ret;
        }
    }
}
