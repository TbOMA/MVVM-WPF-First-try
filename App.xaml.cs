using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using MVVM_FirsTry.Database;
using MVVM_FirsTry.Database.Services;
using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services.AuthenticationServices;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.State.Authenticators;
using MVVM_FirsTry.State.Navigation;
using MVVM_FirsTry.ViewModels;
using MVVM_FirsTry.ViewModels.Factory;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVVM_FirsTry.State.DataOutput;
using MVVM_FirsTry.Services.OrderingService;

namespace MVVM_FirsTry
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
            services.AddSingleton<ApplicationContext>();
            //services.AddSingleton <IDataService<Administrator>,AdminDataService>();
            //services.AddSingleton<IDataService<User>, AccountDataService>();
            //services.AddSingleton<IDataService<Order>, OrderDataService>();
            services.AddScoped<IDataService<User>, AccountDataService>();
            //services.AddScoped<IAccountService>(provider => provider.GetService<AccountDataService>());
            services.AddSingleton<IAccountService, AccountDataService>();
            //services.AddSingleton<IAdminService, AdminDataService>();
            services.AddSingleton<IDataService<Administrator>, AdminDataService>();


            services.AddSingleton<IOrderService, OrderDataService>();
            services.AddSingleton<ICarService,CarDataService>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<CarSelectionViewModel>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<INavigator, Navigator>();
            services.AddScoped<IDataOutput<CarSelectionViewModel>, DataOutput<CarSelectionViewModel>>();
            services.AddScoped<IDataOutput<AdminViewModel>, DataOutput<AdminViewModel>>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IAccountStore, AccountStore>();
            services.AddSingleton<IAdminService,AdminDataService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<MessageViewModel>();
            services.AddSingleton<AdminViewModel>();
            services.AddSingleton<IOrderingService, OrderingService>();  
            services.AddSingleton<IViewModelFactory,ViewModelFactory>();
            services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
            {
                return () => new LoginViewModel(
                    services.GetRequiredService<INavigator>(),
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<IViewModelFactory>());
            });
            services.AddSingleton<CreateViewModel<CarSelectionViewModel>>(services =>
            {
                return () => new CarSelectionViewModel(
                    services.GetRequiredService<ICarService>(),
                    services.GetRequiredService<IOrderingService>(),
                    services.GetRequiredService<IAccountService>(),
                    services.GetRequiredService<IAccountStore>(),
                    services.GetRequiredService<IOrderService>());
            });

            services.AddSingleton<CreateViewModel<AdminViewModel>>(services =>
            {
                return () => new AdminViewModel(
                    services.GetRequiredService<IOrderService>(),
                    services.GetRequiredService<ICarService>()
                    );
            });
            services.AddScoped<MainViewModel>();

            services.AddScoped(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
            return services.BuildServiceProvider();
        }

    }
}
