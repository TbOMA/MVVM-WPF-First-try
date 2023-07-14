using MVVM_FirsTry.Models;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MVVM_FirsTry.State.DataOutput
{
    public class DataOutput<TViewModel> : IDataOutput<TViewModel>
    {
        private readonly TViewModel _viewModel;
        private readonly Image _carImage = new Image();


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
                LoadImageData(viewModel.Cars[counter].ImageData, viewModel);
            }
            catch (Exception)
            {
            }
        }

        public void OrdersDataOutput(int counter)
        {
            try
            {
                dynamic viewModel = _viewModel;
                viewModel.CarId = viewModel.Orders[counter].CarID;
                LoadImageData(viewModel.Orders[counter].Car.ImageData, viewModel);
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
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {
            }
        }

        private void LoadImageData(byte[] imageData, dynamic viewModel)
        {
            if (imageData != null && imageData.Length > 0)
            {
                BitmapImage image = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(imageData))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                viewModel.CarImagePath = image;
            }
        }

    }
}
