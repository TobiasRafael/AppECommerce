using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppECommerce.Interfaces;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppECommerce.Droid.Config))]

namespace AppECommerce.Droid
{
    public class Config : IConfig
    {
        private string _directoryDB;

        public ISQLitePlatform _platform;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(_directoryDB))
                {
                    _directoryDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }

                return _directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (_platform == null)
                {
                    _platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }

                return _platform;

            }
        }


    }
}