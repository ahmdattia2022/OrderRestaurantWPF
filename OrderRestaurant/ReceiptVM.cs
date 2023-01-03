using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderRestaurant
{
    public class ReceiptVM
    {
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public List<Order> Orders { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
