using AppECommerce.Data;
using AppECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppECommerce.Services
{
    public class DataService
    {

        public Response UpdateUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.Update(user);
                }
                return new Response
                {
                    IsSuccess = false,
                    Message = "User updated succesfully!",
                    Result = user,
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        public User GetUser()
        {
            using (var da = new DataAccess())
            {
                return da.First<User>(true);
            }
        }

        public List<Product> GetProducts(string filter)
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Product>(true).OrderBy(p => p.Description)
                    .Where(p => p.Description.ToUpper().Contains(filter.ToUpper()))
                    .ToList();
            }
        }

        public Response InsertUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldUser = da.First<User>(false);
                    if (oldUser != null)
                    {
                        da.Delete(oldUser);
                    }

                    da.Insert(user);
                }
                return new Response
                {
                    IsSuccess = false,
                    Message = "User inserted succesfully!",
                    Result = user,
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

       

       public List<Customer> GetCustomers(string customersFilter)
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Customer>(true).OrderBy(c => c.FirstName)
                    .ThenBy(c => c.LastName)
                    .Where(c => c.FirstName.ToUpper().Contains(customersFilter.ToUpper()) ||
                                c.LastName.ToUpper().Contains(customersFilter.ToUpper()))
                    .ToList();
            }
        }

        public List<Product> GetProducts()
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Product>(true).OrderBy(p => p.Description).ToList();
            }
        }

        public Response Login(string email, string password)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var user = da.First<User>(true);
                    if (user == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No internet connection",
                        };
                    }
                    if (user.UserName.ToUpper() == email.ToUpper() && user.Password == password)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Login Ok",
                            Result = user,
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "User and password incorrect",

                    };
                }
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public void Save<T>(List<T> list)  where T : class
        {
            using (var da = new DataAccess())
            {
                var oldRecords = da.GetList<T>(false);
                foreach (var record in oldRecords)
                {
                    da.Delete(record);
                }
                foreach (var record in list)
                {
                    da.Insert(record);
                }
            }
        }

        public List<T> Get<T>(bool withChildren) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.GetList<T>(withChildren).ToList();
            }
        }
    }
}
