using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Services
{
    public interface ICarService: IDataService<Car>
    {
        Task<IEnumerable<Car>> GetAll();
        
    }
}
