using Microsoft.AspNet.Identity;
using Microsoft.Win32;
using MVVM_FirsTry.Commands;
using MVVM_FirsTry.Database;
using MVVM_FirsTry.Database.Services;
using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using MVVM_FirsTry.Services.OrderingService;
using MVVM_FirsTry.State.DataOutput;
using MVVM_FirsTry.State.Navigation;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MVVM_FirsTry.ViewModels
{
    public class CarSelectionViewModel:ViewModelBase
    {
		private readonly ICarService _carService;
		private readonly IOrderService _orderService;
		private readonly IDataOutput<CarSelectionViewModel> _carDataOutput;
		private readonly IAccountStore _accountStore;
        
        public IEnumerable<Car> Cars { get;  set; }
        public IEnumerable<Order> Orders { get;  set; }

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
		private bool _isNextEnable= true;
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
		private bool _isPayEnable;
		public bool IsPayEnable
        {
			get
			{
				return _isPayEnable;
			}
			set
			{
				_isPayEnable = value;
				OnPropertyChanged(nameof(IsPayEnable));
			}
		}
		private string _carViewPageType= string.Empty;
		public string ViewType
        {
			get
			{
				return _carViewPageType;
			}
			set
			{
				_carViewPageType= value;
				OnPropertyChanged(nameof(ViewType));
			}
		}
        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (value < DateTime.Now)
                {
                    ErrorMessage = "Start date has already occurred!";
					_startDate = DateTime.Now;
                    return;
                }
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        private DateTime _endDate = DateTime.Today;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(TotalAmount));
            }
        }
        public decimal TotalAmount
        {
            get { return Math.Max(RentPrice * (decimal)RentLength.TotalDays, 0); }
            
        }
		private decimal _priceToPay;
		public decimal PriceToPay
		{
			get
			{
				return _priceToPay;
			}
			set
			{
				_priceToPay = value;
				OnPropertyChanged(nameof(PriceToPay));
			}
		}
		public TimeSpan RentLength => EndDate.Subtract(StartDate);

        private OrderStatus _orderStatus ;
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
        private object _currentObject = new Car(); 
        public object CurrentObject
        {
            get { return _currentObject; }
            set { _currentObject = value; }
        }
        private int _currentIndex;
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                CheckPossibilityOfPayment();
            }
        }
        public int UserId { get; set; }
        public ICommand ListingNavigationCommand { get; }
		public ICommand MakeOrderCommand { get; }
        public ICommand NavigationBetweenControlsCommand { get; }
		public ICommand PayForCarCommand { get; }	
        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public CarSelectionViewModel(ICarService carService, 
			IOrderingService orderingService, IAccountService accountService,
			IAccountStore accountStore, IOrderService orderService)
        {
            _carService = carService;
            _accountStore = accountStore;
            _orderService = orderService;
            
            onStart();
            _carDataOutput = new DataOutput<CarSelectionViewModel>(this);
            ListingNavigationCommand = new ListingNavigationCommand<CarSelectionViewModel>(this, LoadCars(),LoadUserOrders());
            MakeOrderCommand = new MakeOrderCommand(this,  orderingService, carService, accountService,accountStore);
            ErrorMessageViewModel = new MessageViewModel();
            NavigationBetweenControlsCommand = new NavigationBetweenControlsCommand<CarSelectionViewModel>(this);
            PayForCarCommand = new PayForCarCommand(this,orderingService);
        }
        public async Task<IEnumerable<Car>> LoadCars()
        {
			
            Cars = await _carService.GetAll();
            CarListingNavigation(0);
			return Cars;

        }
		public async void onStart()
		{
            await _orderService.Delete(6);
            
            /*Car car = await _carService.Get(2);
            User user = _accountStore.CurrentAccount;
            Order order = new Order()
            {
                Car = car,
                User = user,
                CarID = car.Id,
                UserId = user.Id,
                IsPaid = false,
                OrderStatus = OrderStatus.IsProcessed,
                StartTime = DateTime.Now,
                EndTime = DateTime.Today.AddDays(1),
                TotalAmount = 5000,
                RejectionReason = ""
            };


            await _orderService.Create(order);*/
        }
        public void CarListingNavigation(int carsCounter)
        {
            _carDataOutput.CarsDataOutput(carsCounter);
        }
        public void OrderListingNavigation(int ordersCounter)
        {
            _carDataOutput.OrdersDataOutput(ordersCounter);
        }
        public async Task<IEnumerable<Order>> LoadUserOrders()
        {
            Orders = await _orderService.GetAllUserOrdersByPassport(_accountStore.CurrentAccount.PassportNumber);
            OrderListingNavigation(0);
            return Orders;
        }
        

        public void CheckPossibilityOfPayment()
        {
			
                if (CurrentIndex < Orders.Count())
                {
                    var currentOrder = Orders.ElementAt(CurrentIndex);
                    if (!currentOrder.IsPaid && currentOrder.OrderStatus != OrderStatus.Reject &&
                        currentOrder.OrderStatus != OrderStatus.IsProcessed)
                    {
                        IsPayEnable = true;
                    }
                    else
                    {
                        IsPayEnable = false;
                    }
                }
                else
                {
                    IsPayEnable = false;
                }
                
            
        }
    }
}
