using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }

    }
}
