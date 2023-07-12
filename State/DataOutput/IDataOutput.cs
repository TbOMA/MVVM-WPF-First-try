using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.State.DataOutput
{
    public interface IDataOutput<TViewModel>
    {
        void CarsDataOutput(int carsCounter);
        void OrdersDataOutput(int ordersCounter);
    }
}
