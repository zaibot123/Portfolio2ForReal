using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Required { get; set; }    
        public List<OrderDetails>? OrderDetails { get; set; }
        public string? ShipName { get; set; }
        public string? ShipCity { get; set; }
    }
}





