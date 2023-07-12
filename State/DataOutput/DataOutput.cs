using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVM_FirsTry.State.DataOutput
{
    public class DataOutput<TViewModel> :  IDataOutput<TViewModel>
    {
        private readonly TViewModel _viewModel;

        
        public DataOutput(TViewModel viewModel)
        {
            _viewModel = viewModel;
            
        }


        public void CarsDataOutput(int counter)
        {
            dynamic viewModel = _viewModel;
            try
            {
                viewModel.CarId = viewModel.Cars[counter].Id;
                viewModel.CarName = viewModel.Cars[counter].CarName;
                viewModel.CarDescription = viewModel.Cars[counter].Description;
                viewModel.IsAvailable = viewModel.Cars[counter].IsAvailable;
                viewModel.RentPrice = viewModel.Cars[counter].RentPrice;
                viewModel.CarImagePath = viewModel.Cars[counter].CarImagePath;
            }
            catch (Exception)
            {
            }
        }
        public void OrdersDataOutput(int counter)
        {
            dynamic viewModel = _viewModel;
            viewModel.CarId = viewModel.Orders[counter].CarID;
            viewModel.CarImagePath = viewModel.Orders[counter].Car.CarImagePath;
            viewModel.OrderStatus = (OrderStatus)viewModel.Orders[counter].OrderStatus;


            try
            {
                viewModel.CarName = viewModel.Orders[counter].Car.CarName;
                viewModel.IsAvailable = viewModel.Orders[counter].Car.IsAvailable;
                viewModel.PriceToPay = viewModel.Orders[counter].TotalAmount;
                viewModel.CarDescription = viewModel.Orders[counter].Car.Description;

            }
            catch (Exception)
            {
                
            }


            try
            {
                viewModel.RentLength = viewModel.Orders[counter].RentLength.ToString("hh\\:mm\\:ss");
                viewModel.ClientId = viewModel.Orders[counter].UserId;
                viewModel.ClientUsername = viewModel.Orders[counter].User.Username;
                viewModel.PassportNumber = viewModel.Orders[counter].User.PassportNumber;
                viewModel.IsPaid = (bool)viewModel.Orders[counter].IsPaid;
                viewModel.TotalAmount = viewModel.Orders[counter].TotalAmount;
                viewModel.RejectReason = viewModel.Orders[counter].RejectionReason;

                //viewModel.OrderStatusMessage = viewModel.Orders[counter].OrderStatus.ToString();

            }
            catch (Exception)
            {

            }

            

        }


    }
}
