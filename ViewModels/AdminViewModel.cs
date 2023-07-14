using Azure.Core.Pipeline;
using Microsoft.Win32;
using MVVM_FirsTry.Commands;
using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.State.DataOutput;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MVVM_FirsTry.ViewModels
{
    public class AdminViewModel:ViewModelBase
    {
		private readonly IOrderService _orderService;
		private readonly ICarService _carService;
		private readonly IDataOutput<AdminViewModel> _orderDataOutput;
        public IEnumerable<Order> Orders { get; private set; }
        public IEnumerable<Car> Cars { get; set; }

        private string _adminViewType = string.Empty;
		public string ViewType
        {
			get
			{
				return _adminViewType;
			}
			set
			{
				_adminViewType = value;
				OnPropertyChanged(nameof(ViewType));
			}
		}
		private int _carId;
		public int CarId
        {
			get
			{
				return _carId;
			}
			set
			{
				_carId = value;
				OnPropertyChanged(nameof(CarId));
			}
		}
		private int _clientId;
		public int ClientId
        {
			get
			{
				return _clientId;
			}
			set
			{
				_clientId = value;
				OnPropertyChanged(nameof(ClientId));
			}
		}
        private string _rentLength;
        public string RentLength
        {
            get
            {
                return _rentLength;
            }
            set
            {
                _rentLength = value;
                OnPropertyChanged(nameof(RentLength));
            }
        }

        private string _carName;
        public string CarName
        {
            get
            {
                return _carName;
            }
            set
            {
                _carName = value;
                OnPropertyChanged(nameof(CarName));
            }
        }
        private string _carDescription;
        public string CarDescription
        {
            get
            {
                return _carDescription;
            }
            set
            {
                _carDescription = value;
                OnPropertyChanged(nameof(CarDescription));
            }
        }
        private bool _isAvailable;
        public bool IsAvailable
        {
            get
            {
                return _isAvailable;
            }
            set
            {
                _isAvailable = value;
                OnPropertyChanged(nameof(IsAvailable));
            }
        }

        private decimal _rentPrice;
        public decimal RentPrice
        {
            get
            {
                return _rentPrice;
            }
            set
            {
                _rentPrice = value;
                OnPropertyChanged(nameof(RentPrice));
            }
        }
        private string _clientUsername;
		public string ClientUsername
        {
			get
			{
				return _clientUsername;
			}
			set
			{
				_clientUsername = value;
				OnPropertyChanged(nameof(ClientUsername));
			}
		}
		private string _passportNumber;
		public string PassportNumber
        {
			get
			{
				return _passportNumber;
			}
			set
			{
				_passportNumber = value;
				OnPropertyChanged(nameof(PassportNumber));
			}
		}
		private OrderStatus _orderStatus;
		public OrderStatus OrderStatus
        {
			get
			{
				return _orderStatus;
			}
			set
			{
                _orderStatus = value;
				OnPropertyChanged(nameof(OrderStatus));
			}
		}
		private decimal _totalAmount;
		public decimal TotalAmount
        {
			get
			{
				return _totalAmount;
			}
			set
			{
				_totalAmount = value;
				OnPropertyChanged(nameof(TotalAmount));
			}
		}
		private bool _isPaid;
		public bool IsPaid
        {
			get
			{
				return _isPaid;
			}
			set
			{
				_isPaid = value;
				OnPropertyChanged(nameof(IsPaid));
			}
		}
		private string _rejectReason;
		public string RejectReason
        {
			get
			{
				return _rejectReason;
			}
			set
			{
				_rejectReason = value;
				OnPropertyChanged(nameof(RejectReason));
			}
		}
		
		private BitmapImage _carImagePath;
		public BitmapImage CarImagePath
        {
			get
			{
				return _carImagePath;
			}
			set
			{
				_carImagePath = value;
				OnPropertyChanged(nameof(CarImagePath));
			}
		}
        private byte[] _imageData;
        public byte[] ImageData
        {
            get
            {
                return _imageData;
            }
            set
            {
                _imageData = value;
                OnPropertyChanged(nameof(ImageData));
            }
        }
        private bool _isPrevEnable;
        public bool IsPrevEnable
        {
            get
            {
                return _isPrevEnable;
            }
            set
            {
                _isPrevEnable = value;
                OnPropertyChanged(nameof(IsPrevEnable));
            }
        }
        private bool _isNextEnable = true;
        public bool IsNextEnable
        {
            get
            {
                return _isNextEnable;
            }
            set
            {
                _isNextEnable = value;
                OnPropertyChanged(nameof(IsNextEnable));
            }
        }
        public int CurrentIndex { get; set; }
        private object _currentObject = new Order();
        public object CurrentObject
        {
            get { return _currentObject; }
            set { _currentObject = value; }
        }
        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public ICommand NavigationBetweenControlsCommand { get; }
		public ICommand ListingNavigationCommand { get; }
		public ICommand OrderManagingCommand { get; }
        public ICommand EditCarCommand { get; }
        public ICommand LoadImageCommand { get; }
        public ICommand AddCarCommand { get; }

        public AdminViewModel(IOrderService ordersService, ICarService carService)
        {
            _orderService = ordersService;
            _carService = carService;
            NavigationBetweenControlsCommand = new NavigationBetweenControlsCommand<AdminViewModel>(this);
            _orderDataOutput = new DataOutput<AdminViewModel>(this);
            OrderManagingCommand = new OrderManagingCommand(this, ordersService);
            ErrorMessageViewModel = new MessageViewModel();
            ListingNavigationCommand = new ListingNavigationCommand<AdminViewModel>(this,LoadCars(),LoadOrders());
            EditCarCommand = new EditCarCommand(this, _carService);
            AddCarCommand = new AddCarCommand(this, _carService);
            LoadImageCommand = new LoadImageCommand(this);
        }
        public async Task<IEnumerable<Order>> LoadOrders()
        {
            Orders = await _orderService.GetAll();
            OrderListingNavigation(0);
            return Orders;

        }
        public void OrderListingNavigation(int ordersCounter)
        {
            _orderDataOutput.OrdersDataOutput(ordersCounter);
        }
        public async Task<IEnumerable<Car>> LoadCars()
        {

            Cars = await _carService.GetAll();
            CarListingNavigation(0);
            return Cars;

        }

        public void CarListingNavigation(int carsCounter)
        {
            _orderDataOutput.CarsDataOutput(carsCounter);
        }
    }
}
