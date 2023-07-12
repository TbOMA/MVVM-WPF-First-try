using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.Services.AuthenticationServices;
using MVVM_FirsTry.Services.OrderingService;
using MVVM_FirsTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM_FirsTry.Commands
{
    public class MakeOrderCommand : ICommand
    {
        private readonly CarSelectionViewModel _carSelectionViewModel;
        private readonly LoginViewModel _loginViewModel;
        private readonly IOrderingService _orderingService;
        private readonly IDataService<Car> _carService;
        private readonly IAccountService _accountService;

        public MakeOrderCommand(CarSelectionViewModel carSelectionViewModel, LoginViewModel loginViewModel, IOrderingService orderingService, IDataService<Car> carService,  IAccountService accountService)
        {
            _carSelectionViewModel = carSelectionViewModel;
            _loginViewModel = loginViewModel;
            _orderingService = orderingService;
            _carService = carService;
            _accountService = accountService;
        }

        public event EventHandler? CanExecuteChanged;
        
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            Car car = await _carService.Get(_carSelectionViewModel.CarId);
            User user = await _accountService.GetByPassportNumber("1");
            _carSelectionViewModel.ErrorMessage = string.Empty; 
            try
            {
               OrderingResult orderingResult = await _orderingService.MakeOrder(car, user, 
                   _carSelectionViewModel.StartDate, _carSelectionViewModel.EndDate, 
                   _carSelectionViewModel.TotalAmount);
                switch (orderingResult)
                {
                    case OrderingResult.Success:
                        MessageBox.Show("Your order has been successfully placed");
                        break;
                    case OrderingResult.StartDateAlreadyOccurred:
                        _carSelectionViewModel.ErrorMessage = "Start date has already occurred!";
                        break;
                    case OrderingResult.StartDateAfterEndDate:
                        _carSelectionViewModel.ErrorMessage = "The start date can not be after the end date";
                        break;
                    case OrderingResult.InsufficientFunds:
                        _carSelectionViewModel.ErrorMessage = "In your balance insufficient funds";
                        break;
                    default:
                        _carSelectionViewModel.ErrorMessage = "Ordering failed";
                        break;
                }
            }
            catch (Exception)
            {
                _carSelectionViewModel.ErrorMessage = "Ordering failed";
            }
            
            
        }
    }
}
