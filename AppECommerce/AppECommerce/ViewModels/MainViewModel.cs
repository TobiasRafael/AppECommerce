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


        #region Properties

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        

        #endregion


        #region Constructors
        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();

           
            LoadMenu();
        }
        
        #endregion


        #region Methods
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                Icon = "Product.png",
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
