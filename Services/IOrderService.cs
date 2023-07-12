using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Services
{
    public interface IOrderService :IDataService<Order>
    {
        Task<IEnumerable<Order>> GetAllUserOrdersByPassport(string passport);

    }
}
