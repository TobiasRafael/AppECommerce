using AppECommerce.Pages;
using AppECommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using AppECommerce.Models;
using AppECommerce.ViewModels;

namespace AppECommerce
{
    public partial class App : Application
    {

        #region Attributes
        private DataService _dataservice;

        #endregion

        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MainPage Master { get; internal set; }
        public static User CurrentUser { get; internal set; }
        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();

            //Services
            _dataservice = new DataService();
            var user = _dataservice.GetUser();

            if (user != null && user.IsRemembered)
            {
                var mainViewModel = MainViewModel.GetInstace();
                mainViewModel.LoadUser(user);
                App.CurrentUser = user;
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
