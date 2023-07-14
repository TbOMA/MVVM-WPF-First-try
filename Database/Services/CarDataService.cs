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
    public class CarDataService : ICarService
    {
        private readonly ApplicationContext _applicationContext;

        public CarDataService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Create(Car entity)
        {
            _applicationContext.Cars.Add(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Car> Get(int id)
        {
            Car dbrecord = _applicationContext.Cars.Include(a => a.Order).FirstOrDefault(x => x.Id == id);
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            IEnumerable<Car> dbrecord = await  _applicationContext.Cars.Include(a => a.Order).ToListAsync();
            if (dbrecord == null)
            {
                return null;
            }
            return dbrecord;
        }

        public async Task<Car> Update(int id, Car entity)
        {
            try
            {
                Car dbrecord = await _applicationContext.Cars.Include(a => a.Order).FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (dbrecord == null)
                {
                    return dbrecord;
                }
                dbrecord.CarName = entity.CarName;
                dbrecord.RentPrice = entity.RentPrice;
                dbrecord.IsAvailable = entity.IsAvailable;
                dbrecord.Description = entity.Description;
                dbrecord.ImageData = entity.ImageData;
                dbrecord.IsDamaged = entity.IsDamaged;
                dbrecord.DamageDescription = entity.DamageDescription;
                dbrecord.Order = entity.Order;
                dbrecord.OrderId = entity.OrderId;    
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
