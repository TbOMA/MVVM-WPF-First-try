using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.ViewModels;
using System;
using System.Linq;

using System.Windows.Input;

namespace MVVM_FirsTry.Commands
{
    public class EditCarCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly AdminViewModel _adminViewModel;
       
        private readonly IDataService<Car> _carService;
        public EditCarCommand(AdminViewModel adminViewModel,  IDataService<Car> carService)
        {
            _adminViewModel = adminViewModel;
            _carService = carService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                Car car = await _carService.Get(_adminViewModel.Cars.ElementAt(_adminViewModel.CurrentIndex).Id);
                car.RentPrice = _adminViewModel.RentPrice;
                car.CarName = _adminViewModel.CarName;
                car.Description = _adminViewModel.CarDescription;
                car.IsAvailable = _adminViewModel.IsAvailable;
                await _carService.Update(_adminViewModel.Cars.ElementAt(_adminViewModel.CurrentIndex).Id, car);
                _adminViewModel.ErrorMessage = "Car was successfully changed";
            }
            catch (Exception)
            {
                _adminViewModel.ErrorMessage = "Car was not changed!";
            }
        }
    }
}
