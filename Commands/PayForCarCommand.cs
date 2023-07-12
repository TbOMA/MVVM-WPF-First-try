using MVVM_FirsTry.Models;
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
    public class PayForCarCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly CarSelectionViewModel _carSelectionViewModel;
        private readonly IOrderingService _orderingService;

        public PayForCarCommand(CarSelectionViewModel carSelectionViewModel, IOrderingService orderingService)
        {
            _carSelectionViewModel = carSelectionViewModel;
            _orderingService = orderingService;
        }

        public bool CanExecute(object? parameter)
        {
            
            return true;
        }

        public async void Execute(object? parameter)
        {

            try
            {
                PaymentResult paymentResult = await _orderingService.PayForOrder(_carSelectionViewModel.Orders.ElementAt(_carSelectionViewModel.CurrentIndex));
                switch (paymentResult)
                {
                    case PaymentResult.Success:
                        MessageBox.Show("Thank you for your order, come again");
                        _carSelectionViewModel.IsPayEnable = false;
                        break;
                    case PaymentResult.InsufficientFunds:
                        _carSelectionViewModel.ErrorMessage = "In your balance insufficient funds";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                _carSelectionViewModel.ErrorMessage = "Payment failed!";
            }

        }
    }
}
