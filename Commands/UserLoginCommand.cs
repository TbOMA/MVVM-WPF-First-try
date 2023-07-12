using MVVM_FirsTry.State.Authenticators;
using MVVM_FirsTry.State.Navigation;
using MVVM_FirsTry.ViewModels.Factory;
using MVVM_FirsTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVM_FirsTry.Services.AuthenticationServices;
using SimpleTrader.Domain.Exeptions;
using MVVM_FirsTry.Exeptions;

namespace MVVM_FirsTry.Commands
{
    public class UserLoginCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;

        public UserLoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, INavigator navigator, IViewModelFactory viewModelFactory)
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
                await _authenticator.Login(_loginViewModel.PassportNumber);
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.CarSelection);

            }
            catch (UserNotFoundExeption)
            {
                _loginViewModel.ErrorMessage = "Passport number does not exist or it not registered yet";
            }
            catch (EmptyField)
            {
                _loginViewModel.ErrorMessage = "Input passport number";
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessage = "Login failed";
            }
        }
    }
}
