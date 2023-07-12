using MVVM_FirsTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Services.OrderingService
{
    public enum OrderingResult
    {
        Success,
        StartDateAlreadyOccurred,
        StartDateAfterEndDate,
        InsufficientFunds

    }
    public enum PaymentResult
    {
        Success,
        InsufficientFunds
    }
    public interface IOrderingService
    {
        Task<OrderingResult> MakeOrder(Car car, User user, DateTime startTime, DateTime endTime, decimal totalAmount);
        Task<PaymentResult> PayForOrder(Order order);
    }
}
