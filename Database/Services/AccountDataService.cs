using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Database.Services
{
    public class AccountDataService : IAccountService
    {
        private readonly ApplicationContext _applicationContext;

        public AccountDataService( ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Create(User entity)
        {
            _applicationContext.Users.Add(entity);
            await _applicationContext.SaveChangesAsync();
            
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByPassportNumber(string passportNumber)
        {
            User dbrecord = await _applicationContext.Users.Include(a => a.ClientOrders).FirstOrDefaultAsync(a => a.PassportNumber == passportNumber);
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }

        public async Task<User> GetByUsername(string username)
        {
            User dbrecord = await _applicationContext.Users.Include(a => a.ClientOrders).FirstOrDefaultAsync(a => a.Username == username);
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }

        public async Task<User> Update(int id, User entity)
        {
            try
            {
                User dbrecord = _applicationContext.Users.Include(a => a.ClientOrders).FirstOrDefault(x => x.Id == entity.Id);
                if (dbrecord == null)
                {
                    return null;
                }
                dbrecord.Username = entity.Username;
                dbrecord.PassportNumber = entity.PassportNumber;
                dbrecord.Balance = entity.Balance;
                dbrecord.ClientOrders = entity.ClientOrders;
                _applicationContext.SaveChanges();
                return dbrecord;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
