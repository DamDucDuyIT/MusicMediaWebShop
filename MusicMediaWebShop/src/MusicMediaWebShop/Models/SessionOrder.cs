using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MusicMediaWebShop.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models
{
    public class SessionOrder : IOrder
    {
        private ICollection<OrderDetail> _lineItems = new List<OrderDetail>();

        [JsonIgnore]
        public ISession Session { get; set; }
        public ICollection<OrderDetail> LineItems => _lineItems;

        public void AddLineItem(Product product, int quantity)
        {
            OrderDetail line = _lineItems
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            if (line == null)
            {
                _lineItems.Add(new OrderDetail
                {
                    Product = product,
                    Quanlity = quantity
                });
            }
            else
            {
                line.Quanlity += quantity;
            }
            Session.SetJson("Cart", this);
        }

        public void RemoveLineItem(Product product)
        {
            OrderDetail line = _lineItems.SingleOrDefault(l => l.Product.ProductID == product.ProductID);
            _lineItems.Remove(line);
            Session.SetJson("Cart", this);
        }

        public decimal ComputeTotalValue() =>
            _lineItems.Sum(e => e.Product.Price * e.Quanlity);

        public void Clear()
        {
            _lineItems.Clear();
            Session.Remove("Cart");
        }

        public static IOrder GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;
            SessionOrder cart = session?.GetJson<SessionOrder>("Cart")
            ?? new SessionOrder();
            cart.Session = session;
            return cart;
        }
    }
}
