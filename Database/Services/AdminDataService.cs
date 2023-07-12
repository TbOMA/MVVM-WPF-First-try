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
    public class AdminDataService : IAdminService
    {
        private readonly ApplicationContext _applicationContext;

        public AdminDataService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task Create(Administrator entity)
        {
            _applicationContext.Administrators.Add(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Administrator> Get(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Administrator> GetByUsername(string username)
        {
            Administrator dbrecord = await _applicationContext.Administrators.FirstOrDefaultAsync(a => a.Username == username);
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }
        public Task<IEnumerable<Administrator>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Administrator> Update(int id, Administrator entity)
        {
            throw new NotImplementedException();
        }
    }
}
