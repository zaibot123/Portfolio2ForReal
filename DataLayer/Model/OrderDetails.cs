using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Discount { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
    }
}
