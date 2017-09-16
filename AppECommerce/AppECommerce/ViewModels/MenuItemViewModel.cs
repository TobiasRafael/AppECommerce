using AppECommerce.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppECommerce.ViewModels
{
    public class MenuItemViewModel
    {
        #region Attributes
        private NavigationService _navigationService;

        #endregion

        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        
        #endregion

        #region Constructors

        public MenuItemViewModel()
        {
            _navigationService = new NavigationService();
        }

        #endregion

        #region Methods
        private async void Navigate()
        {
          await _navigationService.Navigate(PageName);
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }
               
        #endregion
    }
}
