using Microsoft.Win32;
using MVVM_FirsTry.Models;
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
    public class LoadImageCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly AdminViewModel _adminViewModel;

        public LoadImageCommand(AdminViewModel adminViewModel)
        {
            _adminViewModel = adminViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    byte[] imageData = File.ReadAllBytes(openFileDialog.FileName);
                    _adminViewModel.ImageData = imageData;
                }
                    _adminViewModel.ErrorMessage = "Image has successfully loaded";
            }
            catch (Exception)
            {
                _adminViewModel.ErrorMessage = "Image has not loaded!";
            }
        }
    }
}
