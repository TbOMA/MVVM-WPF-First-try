using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Services
{
    public interface IAccountService : IDataService<User>
    {
        Task<User> GetByPassportNumber(string passportNumber);
        Task<User> GetByUsername(string username);

    }
}
