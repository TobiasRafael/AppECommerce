using AppECommerce.Models;
using AppECommerce.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppECommerce.ViewModels
{
    public class MainViewModel
    {
        #region Attributes

        
        #endregion


        #region Properties

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public LoginViewModel NewLogin { get; set; }

        public UserViewModel UserLoged { get; set; }


        #endregion


        #region Constructors
        public MainViewModel()
        {
            // Singleton
            _instance = this;

            //Observable collections
            Menu = new ObservableCollection<MenuItemViewModel>();
            // Create Views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();

            // Instance services
         
            //Load data
            LoadMenu();
          
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
