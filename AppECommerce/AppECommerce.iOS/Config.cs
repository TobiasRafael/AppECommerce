using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppECommerce.iOS.Config))]
namespace AppECommerce.iOS
{
    public class Config
    {
        private string _directoryDB;
        private ISQLitePlatform _platform;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(_directoryDB))
                {
                    var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    _directoryDB = System.IO.Path.Combine(directory, "..", "Library");
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
                    _platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                }

                return _platform;
            }
        }

    }
}