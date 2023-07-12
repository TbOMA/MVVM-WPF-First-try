using MVVM_FirsTry.Models;
using MVVM_FirsTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_FirsTry.Commands
{
    public class NavigationBetweenControlsCommand<TViewType> : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly TViewType _viewModelType;

        public NavigationBetweenControlsCommand(TViewType viewModelType)
        {
            _viewModelType = viewModelType;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            dynamic viewModel = _viewModelType;
            switch (parameter.ToString())
            {
                case "UserLoginType":
                    viewModel.ViewType = "UserLoginType";
                    break;

                case "UserRegistration":
                    viewModel.ViewType = "UserRegistration";
                    break;

                case "Cancel":
                    viewModel.ViewType = string.Empty;
                    break;

                case "AdminLogin":
                    viewModel.ViewType = "AdminLogin";
                    break;

                case "UserLogin":
                    viewModel.ViewType = "UserLogin";
                    break;

                case "AdminOrdersListing":
                    viewModel.ViewType = "AdminOrdersListing";
                    viewModel.CurrentObject = new Order();
                    await viewModel.LoadOrders();
                    break;
                case "CarEditingView":
                    viewModel.ViewType = "CarEditingView";
                    viewModel.CurrentObject = new Car();
                    await viewModel.LoadCars();
                    break;
                case "CarAddingView":
                    viewModel.ViewType = "CarAddingView";
                    break;
                case "SelectedCar":
                    if (viewModel.Cars[viewModel.CurrentIndex].IsAvailable)
                        viewModel.ViewType = "SelectedCar";
                    else
                        viewModel.ErrorMessage = "Unfortunately, this car is currently unavailable";
                    break;
                case "ShowUserOrders":
                    viewModel.ViewType = "ShowUserOrders";
                    viewModel.CurrentObject = new Order();
                    if(viewModel.CurrentIndex == viewModel.Cars.Count-1)  
                        viewModel.IsNextEnable = true; 
                    viewModel.IsPrevEnable = false;
                    viewModel.CurrentIndex = 0;
                    await viewModel.LoadUserOrders();
                    break;
                case "BackToSelectionCar":
                    viewModel.ViewType = string.Empty;
                    viewModel.CurrentObject = new Car();
                    if (viewModel.CurrentIndex == viewModel.Orders.Count - 1)
                        viewModel.IsNextEnable = true;
                    viewModel.IsPrevEnable = false;
                    viewModel.CurrentIndex = 0;
                    await viewModel.LoadCars();
                    break;
                case "BackToSelectionCarFromOrderPage":
                    viewModel.ViewType = string.Empty;
                    viewModel.ErrorMessage = string.Empty;
                    break;
                default:
                   
                    break;
            }

        }
    }
}
