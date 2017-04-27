using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<OrderDetail> Lines { get; set; }
        public ShippingInfo ShippingInfo { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateShipped { get; set; }
        public string Status { get; set; }
        public Customer Customer { get; set; }
    }
}
