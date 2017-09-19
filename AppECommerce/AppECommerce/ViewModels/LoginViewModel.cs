using AppECommerce.Models;
using AppECommerce.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppECommerce.ViewModels
{
   public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private NavigationService _navigationService;

        private DialogService _dialogService;

        private ApiService _apiService;

        private DataService _dataService;

        private NetService _netService;

        private bool _isRunning;

        private bool _isButtonVisible;

        #endregion

        #region Properties
        public string User { get; set; }

        public string Password { get; set; }

        public bool IsRemembered { get; set; }

        public bool IsRunning {
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }

            get
            {
                return _isRunning;
            }
                
                }

        public bool IsButtonVisible {
            set
            {
                if (_isButtonVisible != value)
                {
                    _isButtonVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsButtonVisible"));
                }
            }

            get
            {
                return _isButtonVisible;
            }

        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            _navigationService = new NavigationService();
            _dialogService = new DialogService();
            _apiService = new ApiService();
            _dataService = new DataService();
            _netService = new NetService();

            IsRemembered = true;
            IsButtonVisible = true;
        } 
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {

            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                await _dialogService.ShowMessage("Empty fields", "May check one of the two files");
                return;
            }
            IsButtonVisible = false;
            IsRunning = true;
            var response = new Response();
            if (_netService.IsConnected())
            {
                response = await _apiService.Login(User, Password);
            }
            else
            {
                response =  _dataService.Login(User, Password);
            }

            IsRunning = false;
            IsButtonVisible = true;

            if (!response.IsSuccess)
            {
                await _dialogService.ShowMessage("Error", response.Message);
                return;
            }
            var user = (User)response.Result;
            user.IsRemembered = IsRemembered;
            user.Password = Password;

            _dataService.InsertUser(user);
            _navigationService.SetMainPage(user);
        } 
        #endregion
    }
}
