using MVVM_FirsTry.State.Authenticators;
using MVVM_FirsTry.State.Navigation;
using MVVM_FirsTry.ViewModels;
using MVVM_FirsTry.ViewModels.Factory;
using SimpleTrader.Domain.Exeptions;
using System;
using System.Windows.Input;

namespace MVVM_FirsTry.Commands
{
    public class AdminLoginCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;

        public AdminLoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            

            
            try
            {
                await _authenticator.Login(_loginViewModel.Username, parameter.ToString());

                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.AdminForm);
            }
            catch (UserNotFoundExeption)
            {
                _loginViewModel.ErrorMessage = "Username does not exist";
            }
            catch (InvalidPasswordExeption)
            {
                _loginViewModel.ErrorMessage = "Incorect password";
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessage = "Login failed";
            }
        }
    }
}
