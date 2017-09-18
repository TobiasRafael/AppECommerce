using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppECommerce.Models
{
   public class Sale
    {
        [PrimaryKey]
        public int SaleId { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

        public int WarehouseId { get; set; }

        public int StateId { get; set; }

        public int? OrderId { get; set; }

        public DateTime Date { get; set; }

        public string Remarks { get; set; }

        [ManyToOne]
        public Customer Customer { get; set; }

        public override int GetHashCode()
        {
            return SaleId;
        }

    }
}
