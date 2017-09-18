using AppECommerce.Interfaces;
using AppECommerce.Models;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppECommerce.Data
{
    public class DataAccess : IDisposable
    {
        private SQLiteConnection _connection;
        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            _connection = new SQLiteConnection(config.Platform,
                System.IO.Path.Combine(config.DirectoryDB, "AppECommerce.db3"));

            //Tables
            _connection.CreateTable<Category>();
            _connection.CreateTable<City>();
            _connection.CreateTable<Company>();
            _connection.CreateTable<CompanyCustomer>();
            _connection.CreateTable<Customer>();
            _connection.CreateTable<Department>();
            _connection.CreateTable<Inventory>();
            _connection.CreateTable<Order>();
            _connection.CreateTable<Product>();
            _connection.CreateTable<Sale>();
            _connection.CreateTable<Tax>();
            _connection.CreateTable<User>();
        }

        public void Insert<T>(T model)
        {
            _connection.Insert(model);
        }

        public void Update<T>(T model)
        {
            _connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            _connection.Delete(model);
        }

        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return _connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return _connection.Table<T>().FirstOrDefault();
            }
        }

        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return _connection.GetAllWithChildren<T>().ToList();
            }
            else
            {
                return _connection.Table<T>().ToList();
            }
        }

        public T Find<T>(int pk, bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return _connection.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return _connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}
