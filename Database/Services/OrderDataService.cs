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
    public class OrderDataService : GenericRepository<Order>, IOrderService
    {
        public OrderDataService(ApplicationContext applicationContext)
       : base(applicationContext)
        {
        }

        public async Task<IEnumerable<Order>> GetAllUserOrdersByPassport(string passport)
        {
            return await _dbSet.Include(a => a.User)
                .Include(b => b.Car)
                .Where(order => order.User.PassportNumber == passport)
                .ToListAsync();
        }
    }
}
