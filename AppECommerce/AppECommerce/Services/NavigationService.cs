using AppECommerce.Models;
using AppECommerce.Pages;
using AppECommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppECommerce.Services
{
   public class NavigationService
    {
        #region Attributes
        private DataService _dataService; 
        #endregion

        public NavigationService()
        {
            _dataService = new DataService();
        }

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;
            switch (pageName)
            {
                case "CustomerPage":
                    await App.Navigator.PushAsync(new CustomerPage());
                    break;
                case "DeliveriesPage":
                    await App.Navigator.PushAsync(new DeliveriesPage());
                    break;
                case "OrdersPage":
                    await App.Navigator.PushAsync(new OrdersPage());
                    break;
                case "ProductsPage":
                    await App.Navigator.PushAsync(new ProductsPage());
                    break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                case "SyncPage":
                    await App.Navigator.PushAsync(new SyncPage());
                    break;
                case "UserPage":
                    await App.Navigator.PushAsync(new UserPage());
                    break;
                case "LogoutPage":
                    Logout();
                    break;
                default:
                    break;
            }
        }

        public User GetCurrentUser()
        {
            return App.CurrentUser;
        }

        private void Logout()
        {
            App.CurrentUser.IsRemembered = false;
            _dataService.UpdateUser(App.CurrentUser);
            App.Current.MainPage = new LoginPage();
        }

        internal void SetMainPage(User user)
        {
            var mainViewModel = MainViewModel.GetInstace();
            mainViewModel.LoadUser(user);
            App.CurrentUser = user;
            App.Current.MainPage = new MainPage();
        }
    }
}
