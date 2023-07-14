using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.Services.OrderingService;
using MVVM_FirsTry.ViewModels;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Windows;
using System.Windows.Input;

namespace MVVM_FirsTry.Commands
{
    public class MakeOrderCommand : ICommand
    {
        private readonly CarSelectionViewModel _carSelectionViewModel;
        private readonly IOrderingService _orderingService;
        private readonly IDataService<Car> _carService;
        private readonly IAccountService _accountService;
        private readonly IAccountStore _accountStore;

        public MakeOrderCommand(CarSelectionViewModel carSelectionViewModel, IOrderingService orderingService, IDataService<Car> carService, IAccountService accountService, IAccountStore accountStore)
        {
            _carSelectionViewModel = carSelectionViewModel;
            _orderingService = orderingService;
            _carService = carService;
            _accountService = accountService;
            _accountStore = accountStore;
        }

        public event EventHandler? CanExecuteChanged;
        
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            Car car = await _carService.Get(_carSelectionViewModel.CarId);
            User user = await _accountService.GetByPassportNumber(_accountStore.CurrentAccount.PassportNumber);
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
