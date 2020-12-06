using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TDM.DAL;
using TDM.DAL.Services;
using TDM.DAL.Services.MongoDB;
using TDM.Domain.Models;
using TDM.Domain.Services;
using TDM.Domain.Services.AuthServices;
using TDM.Domain.Services.MongoDB;
using TDM.Presentation.State.Navigators;
using TDM.Presentation.ViewModels;
using TDM.Presentation.ViewModels.ViewModelFactories;
using TDM.Services.Services;
using Tweetinvi;

namespace TDM.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();
            base.OnStartup(e);
         }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            
            // Confidential Information
            string consumerKey = ConfigurationManager.AppSettings.Get("consumerKey");
            string consumerSecret = ConfigurationManager.AppSettings.Get("consumerSecret");

            string userName = ConfigurationManager.AppSettings.Get("userName");
            string userPassword = ConfigurationManager.AppSettings.Get("userPassword");
            string dbName = ConfigurationManager.AppSettings.Get("dbName");
            string collectionName = ConfigurationManager.AppSettings.Get("collectionName");
            string serverPath = ConfigurationManager.AppSettings.Get("serverPath");


            services.AddSingleton<IDataService<User>, GenericDataService<User>>();
            services.AddSingleton<ITweetService, TweetRepository>();
            services.AddSingleton<IDataService<Tweet>, GenericDataService<Tweet>>();
            services.AddSingleton<DBContextFactory>();
            services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IUserDataService, UserRepository>();

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();

            services.AddSingleton<IMongoDbRepo, MongoDbRepo>(s =>
            {
                return new MongoDbRepo(serverPath, userName, dbName, userPassword, collectionName);
            });

            services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
            {
                return () => services.GetRequiredService<LoginViewModel>();
            });

            services.AddSingleton<CreateViewModel<HomeViewModel>>(s =>
            {
                return () => new HomeViewModel(consumerKey, consumerSecret, s.GetRequiredService<ITweetService>(), s.GetRequiredService<IMongoDbRepo>());
            });

            services.AddSingleton<CreateViewModel<NewUserViewModel>>(services =>
            {
                return () => new NewUserViewModel(consumerKey, consumerSecret, services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(), services.GetRequiredService<IDataService<User>>());
            });
            
            services.AddSingleton<CreateViewModel<ExistingUserViewModel>>(s =>
            {
                return () => new ExistingUserViewModel(s.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(), s.GetRequiredService<IAuthenticationService>());
            });

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<ExistingUserViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
