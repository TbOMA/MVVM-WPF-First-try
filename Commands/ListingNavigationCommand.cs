using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_FirsTry.Commands
{

    public class ListingNavigationCommand<TViewModel> : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly TViewModel _viewModel;
        private Task<IEnumerable<object>> _items;
        private object _currentObject;
        private readonly Task<IEnumerable<Car>> _cars;
        private readonly Task<IEnumerable<Order>> _orders;

        public ListingNavigationCommand(TViewModel viewModel, Task<IEnumerable<Car>> cars, Task<IEnumerable<Order>> orders)
        {
            _viewModel = viewModel;
            _cars = cars;
            _orders = orders;
        }

        public bool CanExecute(object? parameter) => true;

        public async void Execute(object parameter)
        {
            dynamic viewModel = _viewModel;
            var cars = await _cars;
            var orders = await _orders;
            _currentObject = viewModel.CurrentObject;

            if (_currentObject.GetType() == typeof(Car))
                _items = Task.FromResult(cars.Select(car => (object)car));
            else if (_currentObject.GetType() == typeof(Order))
                _items = Task.FromResult(orders.Select(order => (object)order));

            int _counter = viewModel.CurrentIndex;

            if (parameter.ToString() == "Next")
            {
                _counter++;
                viewModel.IsNextEnable = _counter != (await _items).Count() - 1;
                viewModel.IsPrevEnable = _counter >= 1;

                if (_currentObject.GetType() == typeof(Car))
                {
                    viewModel.CarListingNavigation(_counter);
                    viewModel.ErrorMessage = string.Empty;
                }
                else if (_currentObject.GetType() == typeof(Order))
                {
                    viewModel.OrderListingNavigation(_counter);
                    viewModel.ErrorMessage = string.Empty;
                }
            }
            else if (parameter.ToString() == "Prev")
            {
                _counter--;
                viewModel.IsNextEnable = true;
                viewModel.IsPrevEnable = _counter > 0;

                if (_currentObject.GetType() == typeof(Car))
                {
                    viewModel.CarListingNavigation(_counter);
                    viewModel.ErrorMessage = string.Empty;
                }
                else if (_currentObject.GetType() == typeof(Order))
                    viewModel.OrderListingNavigation(_counter);
            }

            viewModel.CurrentIndex = _counter;
        }
    }



}
