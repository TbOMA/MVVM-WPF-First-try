using Microsoft.EntityFrameworkCore;
using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Database.Services
{
    public class CarDataService : GenericRepository<Car>, ICarService
    {

        public CarDataService(ApplicationContext applicationContext)
       : base(applicationContext)
        {
        }
    }
}
