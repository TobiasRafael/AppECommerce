using AppECommerce.Models;
using AppECommerce.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;
using System;

namespace AppECommerce.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes

        private ApiService _apiService;

        private NetService _netService;

        private DataService _dataService;

        private string _productsFilter;

        private string _customerFilter;

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<ProductItemViewModel> Products { get; set; }

        public ObservableCollection<CustomerItemViewModel> Customers { get; set; }

        public LoginViewModel NewLogin { get; set; }

        public UserViewModel UserLoged { get; set; }

        public CustomerItemViewModel CurrentCustomer { get; set; }

        public string ProductsFilter
        {
            set
            {
                if (_productsFilter != value)
                {
                    _productsFilter = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductsFilter"));
                    if (string.IsNullOrEmpty(_productsFilter))
                    {
                        LoadLocalProducts();
                    }
                }
            }
            get
            {
                return _productsFilter;
            }
        }
   
        public string CustomersFilter
        {
            set
            {
                if (_customerFilter != value)
                {
                    _customerFilter = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CustomersFilter"));
                    if (string.IsNullOrEmpty(_customerFilter))
                    {
                        LoadLocalCustomers();
                    }
                }
            }
            get
            {
                return _customerFilter;
            }
        }

        #endregion

        #region Commands

        public ICommand SearchProductCommand { get { return new RelayCommand(SearchProduct); } }
        public ICommand SearchCustomerCommand { get { return new RelayCommand(SearchCustomer); } }

        private void SearchCustomer()
        {
            var customers = _dataService.GetCustomers(CustomersFilter);
            ReloadCustomers(customers);
        }

        private void SearchProduct()
        {

            var products = _dataService.GetProducts(ProductsFilter);

            ReloadProducts(products);

        }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            // Singleton
            _instance = this;

            //Observable collections
            Menu = new ObservableCollection<MenuItemViewModel>();
            Products = new ObservableCollection<ProductItemViewModel>();
            Customers = new ObservableCollection<CustomerItemViewModel>();

            // Create Views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();
            CurrentCustomer = new CustomerItemViewModel();

            // Instance services
            _apiService = new ApiService();
            _netService = new NetService();
            _dataService = new DataService();

            //Load data
            LoadMenu();
            LoadProducts();
            LoadCustomers();

        }

        #endregion

        #region Singleton

        private static MainViewModel _instance;



        public static MainViewModel GetInstace()
        {
            if (_instance == null)
            {
                _instance = new MainViewModel();
            }
            return _instance;
        }
        #endregion


        #region Methods

        public void SetCurrentCustomer(CustomerItemViewModel customerItemViewModel)
        {
            CurrentCustomer = customerItemViewModel;
        }
        private void LoadLocalCustomers()
        {
            var customers = _dataService.Get<Customer>(true);
            ReloadCustomers(customers);
        }


        private async void LoadCustomers()
        {
            var customers = new List<Customer>();

            if (_netService.IsConnected())
            {
                customers = await _apiService.Get<Customer>("Customers");
                _dataService.Save(customers);
            }
            else
            {
                customers = _dataService.Get<Customer>(true);
            }

            ReloadCustomers(customers);
        }

        private void ReloadCustomers(List<Customer> customers)
        {
            Customers.Clear();
            foreach (var customer in customers.OrderBy(c => c.FirstName).ThenBy(c => c.LastName))
            {
                Customers.Add(new CustomerItemViewModel
                {
                    CustomerId = customer.CustomerId,
                    UserName = customer.UserName,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Photo = customer.Photo,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    Latitude = customer.Latitude,
                    Longitude = customer.Longitude,
                    Department = customer.Department,
                    DepartmentId = customer.DepartmentId,
                    CompanyCustomers = customer.CompanyCustomers,
                    IsUpdated = customer.IsUpdated,
                    City = customer.City,
                    CityId = customer.CityId,
                    Sales = customer.Sales,
                    Orders = customer.Orders,

                });
            }
        }

        private void LoadLocalProducts()
        {
            var products = _dataService.Get<Product>(true);
            ReloadProducts(products);
        }

        private async void LoadProducts()
        {
            var products = new List<Product>();

            if (_netService.IsConnected())
            {
                products = await _apiService.Get<Product>("Products");
                _dataService.Save(products);
            }
            else
            {
                products = _dataService.Get<Product>(true);
            }

            ReloadProducts(products);

        }

        private void ReloadProducts(List<Product> products)
        {
            Products.Clear();
            foreach (var product in products.OrderBy(p => p.Description))
            {
                Products.Add(new ProductItemViewModel
                {
                    BarCode = product.BarCode,
                    Category = product.Category,
                    CategoryId = product.CategoryId,
                    Company = product.Company,
                    CompanyId = product.CompanyId,
                    Description = product.Description,
                    Image = product.Image,
                    Inventories = product.Inventories,
                    Price = product.Price,
                    ProductId = product.ProductId,
                    Remarks = product.Remarks,
                    Stock = product.Stock,
                    Tax = product.Tax,
                    TaxId = product.TaxId,
                });
            }
        }

        public void LoadUser(User user)
        {
            UserLoged.FullName = user.FullName;
            UserLoged.Photo = user.PhotoFullPath;


        }


        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                Icon = "Products.png",
                PageName = "ProductsPage",
                Title = "Products",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "User.png",
                PageName = "CustomerPage",
                Title = "Customer",
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "Orders.png",
                PageName = "OrdersPage",
                Title = "Orders",
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "Delivery.png",
                PageName = "DeliveriesPage",
                Title = "Delivery",
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "Sync.png",
                PageName = "SyncPage",
                Title = "Sync",
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "Setup.png",
                PageName = "SetupPage",
                Title = "Setup",
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "Logout.png",
                PageName = "LogoutPage",
                Title = "Logout",
            });
        }
        #endregion
    }
}
