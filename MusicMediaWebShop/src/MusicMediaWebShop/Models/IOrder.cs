using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public interface IOrder
    {
        //[BindNever]
        //int OrderID { get; set; }

        void AddLineItem(Product product, int quantity);
        
        void RemoveLineItem(Product product);

        decimal ComputeTotalValue();

        void Clear();

        ICollection<OrderDetail> LineItems { get; }
        //ShippingInfo ShippingInfo { get; set; }

        //[BindNever]
        //bool Shipped { get; set; }

        //[Required(ErrorMessage = "Please enter a name")]
        //string Name { get; set; }

        //DateTime DateCreated { get; set; }
        //DateTime DateShipped { get; set; }
        //string Status { get; set; }
        //Customer Customer { get; set; }
    }
}
