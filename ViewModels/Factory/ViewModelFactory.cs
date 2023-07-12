using MVVM_FirsTry.State.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.ViewModels.Factory
{
    public class ViewModelFactory : IViewModelFactory
    {

        
        private readonly CreateViewModel<LoginViewModel> _loginViewModel;
        private readonly CreateViewModel<CarSelectionViewModel> _carSelectionViewModel;
        private readonly CreateViewModel<AdminViewModel> _adminViewModel;

        public ViewModelFactory(CreateViewModel<LoginViewModel> createLoginViewModelFactory, 
            CreateViewModel<CarSelectionViewModel> carSelectionViewModel, 
            CreateViewModel<AdminViewModel> adminViewModel)
        {

            _loginViewModel = createLoginViewModelFactory;
            _carSelectionViewModel = carSelectionViewModel;
            _adminViewModel = adminViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _loginViewModel();
               case ViewType.CarSelection:
                   return _carSelectionViewModel(); 
                case ViewType.AdminForm: 
                    return _adminViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel", "viewType");
            }
        }
    }
}
