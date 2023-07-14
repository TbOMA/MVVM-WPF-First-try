using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services.AuthenticationServices;
using MVVM_FirsTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Services.OrderingService
{
    public class OrderingService : IOrderingService
    {
        
        private readonly IOrderService _orderingService;
        private readonly ICarService _carService;
        private readonly IAccountService _accountService;
        public OrderingService(IOrderService orderingService, ICarService carService, IAccountService accountService)
        {
            _orderingService = orderingService;
            _carService = carService;
            _accountService = accountService;
        }

        public async Task<OrderingResult> MakeOrder(Car car, User user,DateTime startTime,DateTime endTime,decimal totalAmount)
        {
            OrderingResult result = OrderingResult.Success;
            if (user.Balance<totalAmount)
            {
                result = OrderingResult.InsufficientFunds;
            }
            if (startTime<=DateTime.Today)
            {
                result = OrderingResult.StartDateAlreadyOccurred;
            }
            if (startTime>endTime)
            {
                result = OrderingResult.StartDateAfterEndDate;
            }
            if (result == OrderingResult.Success)
            {
                Order order = new Order()
                {
                    Car = car,
                    User = user,
                    CarID = car.Id,
                    UserId = user.Id,
                    IsPaid = false,
                    OrderStatus = OrderStatus.IsProcessed,
                    StartTime = startTime,
                    EndTime = endTime,
                    TotalAmount = totalAmount,
                    RejectionReason = ""
                };


                //user.ClientOrders.Clear();
                await _orderingService.Create(order);
                user.ClientOrders.Add(order);
                car.IsAvailable = false;
                await _carService.Update(car.Id, car);
            }


            return result;
        }
        public async Task<PaymentResult> PayForOrder(Order order)
        {
            PaymentResult result = PaymentResult.Success;
            if (order.User.Balance < order.TotalAmount)
            {
                result = PaymentResult.InsufficientFunds;
            }
            if (result==PaymentResult.Success)
            {
                order.User.Balance = order.User.Balance - order.TotalAmount;
                order.IsPaid = true;
                await _orderingService.Update(order.Id, order);
                await _accountService.Update(order.User.Id, order.User);
            }
            return result;
        }

    }
}
