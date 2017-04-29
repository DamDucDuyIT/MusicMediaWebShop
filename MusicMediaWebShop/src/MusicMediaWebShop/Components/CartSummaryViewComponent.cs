using Microsoft.AspNetCore.Mvc;
using MusicMediaWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private IOrder _cart;

        public CartSummaryViewComponent(IOrder cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
