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
    public class OrderDataService :IOrderService
    {
        private readonly ApplicationContext _applicationContext;

        public OrderDataService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task Create(Order entity)
        {
            _applicationContext.Orders.Add(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            using (var context = new ApplicationContext())
            {
                IEnumerable<Order> dbrecord = await context.Orders.Include(a => a.User).Include(b => b.Car).ToListAsync();
                if (dbrecord == null)
                {
                    return null;
                }
                return dbrecord;
            }
        }

        public async Task<IEnumerable<Order>> GetAllUserOrdersByPassport(string passport)
        {
            using (var context = new ApplicationContext())
            {
                List<Order> matchingOrders = await context.Orders
                    .Include(a => a.User)
                    .Include(b => b.Car)
                    .Where(order => order.User.PassportNumber == passport)
                    .ToListAsync();

                return matchingOrders;
            }
        }


        public async Task<Order> Update(int id, Order entity)
        {
            try
            {
                Order dbrecord = await _applicationContext.Orders.Include(a => a.Car).Include(b => b.User).FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (dbrecord == null)
                {
                    return dbrecord;
                }

                dbrecord.OrderStatus = entity.OrderStatus;
                dbrecord.RejectionReason = entity.RejectionReason;
                dbrecord.EndTime = entity.EndTime;
                dbrecord.StartTime = entity.StartTime;
                dbrecord.IsPaid = entity.IsPaid;
                dbrecord.TotalAmount = entity.TotalAmount;


                _applicationContext.Entry(dbrecord.Car).CurrentValues.SetValues(entity.Car);


                _applicationContext.Entry(dbrecord.User).CurrentValues.SetValues(entity.User);

                _applicationContext.Update(dbrecord);
                await _applicationContext.SaveChangesAsync();

                return dbrecord;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
