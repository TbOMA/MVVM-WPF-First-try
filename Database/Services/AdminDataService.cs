using Microsoft.EntityFrameworkCore;
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
    public class AdminDataService : GenericRepository<Administrator>, IAdminService
    {
        public AdminDataService(ApplicationContext applicationContext)
        : base(applicationContext)
        {
        }

        public async Task<Administrator> GetByUsername(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Username == username);
        }
    }
}
