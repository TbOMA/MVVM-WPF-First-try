using MVVM_FirsTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.State.Navigation
{
    public enum ViewType
    {
        Login,
        CarSelection,
        AdminForm
    }
   public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;

    }
}
