using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models.Repository
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<ShippingInfo>> RegistrationsAsync();

        //IEnumerable<Order> NotYetShippedOrders { get; }

        Task<IEnumerable<ShippingInfo>> UnpaidRegistrationsAsync();

        //void SaveOrder(Order order);
        Task SaveOrderAsync(ShippingInfo purchase);

        //Order Search(int orderID);

        Task<ShippingInfo> SearchAsync(int purchaseID);
    }
}
