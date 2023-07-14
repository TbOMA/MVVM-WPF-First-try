using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;

namespace MVVM_FirsTry.Commands
{
    public class OrderManagingCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly AdminViewModel _adminViewModel;
        private readonly IDataService<Order> _orderingService;
        public OrderManagingCommand(AdminViewModel adminViewModel, IDataService<Order> orderingService)
        {
            _adminViewModel = adminViewModel;
            _orderingService = orderingService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter.ToString() == "Confirm")
            {
                _adminViewModel.Orders.ElementAt(_adminViewModel.CurrentIndex).OrderStatus = OrderStatus.Confirm;
                _adminViewModel.OrderStatus = OrderStatus.Confirm;
                _adminViewModel.Orders.ElementAt(_adminViewModel.CurrentIndex).RejectionReason = "-";
                _orderingService.Update(_adminViewModel.Orders.ElementAt(_adminViewModel.CurrentIndex).Id, _adminViewModel.Orders.ElementAt(_adminViewModel.CurrentIndex));
            }
            if (parameter.ToString() == "Reject")
            {
                _adminViewModel.Orders.ElementAt(_adminViewModel.CurrentIndex).OrderStatus = OrderStatus.Reject;
                _adminViewModel.OrderStatus = OrderStatus.Reject;
                _adminViewModel.Orders.ElementAt(_adminViewModel.CurrentIndex).RejectionReason = _adminViewModel.RejectReason;
                _orderingService.Update(_adminViewModel.Orders.ElementAt(_adminViewModel.CurrentIndex).Id, _adminViewModel.Orders.ElementAt(_adminViewModel.CurrentIndex));
            }
        }
    }
}
