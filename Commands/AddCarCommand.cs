using Microsoft.Win32;
using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_FirsTry.Commands
{
    public class AddCarCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly  AdminViewModel _adminViewModel;
        private readonly ICarService _carService;

        public AddCarCommand(AdminViewModel adminViewModel, ICarService carService)
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
                Car car = new Car()
                {
                    IsAvailable = _adminViewModel.IsAvailable,
                    RentPrice = _adminViewModel.RentPrice,
                    IsDamaged = false,
                    DamageDescription = "",
                    CarName = _adminViewModel.CarName,
                    Description = _adminViewModel.CarDescription,
                    ImageData = _adminViewModel.ImageData
                };
                await _carService.Create(car);
                _adminViewModel.ErrorMessage = "Car was successfully added";
            }
            catch (Exception)
            {
                _adminViewModel.ErrorMessage = "Car was not added";
            }

        }
    }
}
