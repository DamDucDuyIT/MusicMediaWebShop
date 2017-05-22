using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicMediaWebShop.Models.Repository;
using MusicMediaWebShop.Models;
using MusicMediaWebShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicMediaWebShop.Controllers
{
   
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private IOrder _cart;

        public CartController(IProductRepository repo, IOrder cart)
        {
            _repository = repo;
            _cart = cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new OrderIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public async Task<RedirectToActionResult> AddToCart(int productID, string returnUrl)
        {
            Product product = await _repository.SearchAsync(productID);
            if (product != null)
            {
                _cart.AddLineItem(product, 1);
            }
            ViewData["NumberOfCart"] = _cart.LineItems.Count;
            return RedirectToAction("Index", new { returnUrl });
        }

        public async Task<RedirectToActionResult> RemoveFromCart(int courseId, string returnUrl)
        {
            Product product = await _repository.SearchAsync(courseId);
            if (product != null)
            {
                _cart.RemoveLineItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
