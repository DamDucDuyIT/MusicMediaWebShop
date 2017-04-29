using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicMediaWebShop.Models.Repository;
using MusicMediaWebShop.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicMediaWebShop.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: /<controller>/
        private IPurchaseRepository _repository;
        private IOrder _cart;

        public PurchaseController(IPurchaseRepository repository, IOrder cart)
        {
            _repository = repository;
            _cart = cart;
        }

        public ViewResult Checkout() => View(new ShippingInfo());

        [HttpPost]
        public async Task<IActionResult> Checkout(ShippingInfo purchase)
        {
            if (_cart.LineItems.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                purchase.LineItems = _cart.LineItems;
                await _repository.SaveOrderAsync(purchase);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(purchase);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }

        [Authorize]
        public async Task<ViewResult> List() =>
            View(await _repository.UnpaidRegistrationsAsync());

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MarkSent(int registrationID)
        {
            ShippingInfo purchase = await _repository.SearchAsync(registrationID);
            if (purchase != null)
            {
                purchase.Sent = true;
                await _repository.SaveOrderAsync(purchase);
            }
            return RedirectToAction(nameof(List));
        }
    }
}
