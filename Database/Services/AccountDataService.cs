using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Database.Services
{
    public class AccountDataService : GenericRepository<User>, IAccountService
    {
        public AccountDataService(ApplicationContext applicationContext)
        : base(applicationContext)
        {
        }
        public async Task<User> GetByPassportNumber(string passportNumber)
        {
            return await _dbSet.Include(a => a.ClientOrders)
                .FirstOrDefaultAsync(a => a.PassportNumber == passportNumber);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _dbSet.Include(a => a.ClientOrders)
                .FirstOrDefaultAsync(a => a.Username == username);
        }
    }
}
