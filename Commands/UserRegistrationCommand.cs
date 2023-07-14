using MVVM_FirsTry.Services.AuthenticationServices;
using MVVM_FirsTry.State.Authenticators;
using MVVM_FirsTry.State.Navigation;
using MVVM_FirsTry.ViewModels;
using MVVM_FirsTry.ViewModels.Factory;
using System;
using System.Windows.Input;

namespace MVVM_FirsTry.Commands
{
    public class UserRegistrationCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        public UserRegistrationCommand(INavigator navigator, LoginViewModel loginViewModel, IAuthenticator authenticator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
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
                RegistrationResult registrationResult = await _authenticator.Register(_loginViewModel.Username, _loginViewModel.PassportNumber,_loginViewModel.DepositAmount);
                switch (registrationResult)
                {
                    case RegistrationResult.Success:
                        _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.CarSelection);
                        break;
                    case RegistrationResult.PassportAlreadyExists:
                        _loginViewModel.ErrorMessage = "An account for this passport number already exist";
                        break;
                    case RegistrationResult.UsernameAlreadyExists:
                        _loginViewModel.ErrorMessage = "An account for this username already exist";
                        break;
                    case RegistrationResult.DepositIncorrect:
                        _loginViewModel.ErrorMessage = "Incorrect deposit";
                        break;
                    case RegistrationResult.EmptyPassportNumber:
                        _loginViewModel.ErrorMessage = "Enter your passport number";
                        break;
                    case RegistrationResult.EmptyUsername:
                        _loginViewModel.ErrorMessage = "Enter your username";
                        break;
                    default:
                        _loginViewModel.ErrorMessage = "Registration failed";
                        break;
                }
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessage = "Registration failed";
            }
        }
    }
}
