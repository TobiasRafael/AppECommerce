using AppECommerce.Models;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using AppECommerce.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System;

namespace AppECommerce.ViewModels
{
    public class CustomerItemViewModel : Customer
    {

        #region Attributes
        private NavigationService _navigationService;
        private NetService _netService;
        private ApiService _apiService;
        private DataService _dataService;
        #endregion

        #region Properties

        public ObservableCollection<DepartmentItemViewModel> Departments { get; set; }

        public ObservableCollection<CityItemViewModel> Cities { get; set; }

        #endregion

        #region Constructors
        public CustomerItemViewModel()
        {
            //Services
            _navigationService = new NavigationService();
            _netService = new NetService();
            _apiService = new ApiService();
            _dataService = new DataService();

            //Observable collections
            Departments = new ObservableCollection<DepartmentItemViewModel>();
            Cities = new ObservableCollection<CityItemViewModel>();

            //LoadData
            LoadDepartments();
            LoadCities();
        }




        #endregion

        #region Commands
        public ICommand CustomerDetailCommand { get { return new RelayCommand(CustomerDetail); } }

        private async void CustomerDetail()
        {
            var customerItemViewModel = new CustomerItemViewModel
            {
                CustomerId = CustomerId,
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Photo = Photo,
                Phone = Phone,
                Address = Address,
                Latitude = Latitude,
                Longitude = Longitude,
                Department = Department,
                DepartmentId = DepartmentId,
                CompanyCustomers = CompanyCustomers,
                IsUpdated = IsUpdated,
                City = City,
                CityId = CityId,
                Sales = Sales,
                Orders = Orders,
            };

            var mainViewModel = MainViewModel.GetInstace();
            mainViewModel.SetCurrentCustomer(customerItemViewModel);
            await _navigationService.Navigate("CustomerDetailPage");
        }
        #endregion
        #region Methods

        private async void LoadCities()
        {
            var cities = new List<City>();

            if (_netService.IsConnected())
            {
                cities = await _apiService.Get<City>("Cities");
                _dataService.Save(cities);
            }
            else
            {
                cities = _dataService.Get<City>(true);
            }

            ReloadCities(cities);
        }

        private void ReloadCities(List<City> cities)
        {
            Cities.Clear();

            foreach (var city in cities)
            {
                Cities.Add(new CityItemViewModel
                {
                    CityId = city.CityId,
                    Name = city.Name,
                    Customers = city.Customers,
                    Department = city.Department,
                    DepartmentId = city.DepartmentId,

                });
            }
        }

        private async void LoadDepartments()
        {
            var departments = new List<Department>();

            if (_netService.IsConnected())
            {
                departments = await _apiService.Get<Department>("Departments");
                _dataService.Save(departments);
            }
            else
            {
                departments = _dataService.Get<Department>(true);
            }

            ReloadDepartments(departments);
        }

        private void ReloadDepartments(List<Department> departments)
        {
            Departments.Clear();
            foreach (var department in departments.OrderBy(d => d.Name))
            {
                Departments.Add(new DepartmentItemViewModel
                {
                    Cities = department.Cities,
                    Customers = department.Customers,
                    DepartmentId = department.DepartmentId,
                    Name = department.Name,
                });

            }
        }

        #endregion


    }
}
