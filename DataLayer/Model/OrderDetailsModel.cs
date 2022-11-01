using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class OrderDetailsModel
    {
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public string Name { get; set; }
    }
}
