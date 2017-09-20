using AppECommerce.ViewModels;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppECommerce.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerDetailPage : ContentPage
    {
        public CustomerDetailPage()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstace();
            mainViewModel.SetGeolotation(
            mainViewModel.CurrentCustomer.FullName,
            mainViewModel.CurrentCustomer.Address,
            mainViewModel.CurrentCustomer.Latitude,
            mainViewModel.CurrentCustomer.Longitude);
            foreach (var pin in mainViewModel.Pins)
            {
                MyMap.Pins.Add(pin);
            }

            Locator();
        }

        private async void Locator()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            var position = new Position(location.Latitude, location.Longitude);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.3)));

        }
    }
}