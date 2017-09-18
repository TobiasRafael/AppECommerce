using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppECommerce.Models
{
   public class Department
    {
        [PrimaryKey]
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<City> Cities { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Customer> Customers { get; set; }

        public override int GetHashCode()
        {
            return DepartmentId;
        }
    }
}
