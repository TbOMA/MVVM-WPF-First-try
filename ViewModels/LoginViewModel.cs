using MVVM_FirsTry.Commands;
using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.State.Authenticators;
using MVVM_FirsTry.State.DataOutput;
using MVVM_FirsTry.State.Navigation;
using MVVM_FirsTry.ViewModels.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_FirsTry.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private string _loginType = string.Empty;
        public string ViewType
        {
            get
            {
                return _loginType;
            }
            set
            {
                _loginType = value;
                OnPropertyChanged(nameof(ViewType));
            }
        }
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _passportNumber;
        public string PassportNumber
        {
            get
            {
                return _passportNumber;
            }
            set
            {
                _passportNumber = value;
                OnPropertyChanged(nameof(PassportNumber));
            }
        }
        private decimal _depositAmount;
        public decimal DepositAmount
        {
            get
            {
                return _depositAmount;
            }
            set
            {
                _depositAmount = value;
                OnPropertyChanged(nameof(DepositAmount));
            }
        }
        
        public ICommand NavigationBetweenControlsCommand { get; }
        public ICommand UserRegistrationCommand { get; }
        public ICommand AdminLoginCommand { get; }
        public ICommand UserLoginCommand { get; }
        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public LoginViewModel(INavigator navigator, IAuthenticator authenticator, IViewModelFactory viewModelFactory)
        {


            UserRegistrationCommand = new UserRegistrationCommand(navigator, this, authenticator, viewModelFactory);
            ErrorMessageViewModel = new MessageViewModel();
            NavigationBetweenControlsCommand = new NavigationBetweenControlsCommand<LoginViewModel>(this);
            AdminLoginCommand = new AdminLoginCommand(this, authenticator, navigator, viewModelFactory);
            UserLoginCommand = new UserLoginCommand(this, authenticator, navigator, viewModelFactory);
        }
        /*public async Task<IEnumerable<Order>> LoadUserOrders()
        {
            var userOrders = await _orderService.GetAllUserOrdersByPassport(PassportNumber);
            return userOrders;
        }*/
    }
}
