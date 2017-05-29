using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicMediaWebShop.Models.Repository;
using MusicMediaWebShop.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using MusicStore.Services.NganLuongAPI;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicMediaWebShop.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: /<controller>/
        private IPurchaseRepository _repository;
        private IOrder _cart;
        private const string PromoCode = "FREE";

        private string merchantId = "50965";
        private string merchantPassword = "c5a202bd46d196d76cd8c4955b260e08";
        private string merchantEmail = "tan.do.coder@gmail.com";

        public PurchaseController(IPurchaseRepository repository, IOrder cart)
        {
            _repository = repository;
            _cart = cart;
        }

        public ViewResult Checkout() => View(new ShippingInfo());

        [HttpPost]

        public async Task<IActionResult> Checkout([FromForm]ShippingInfo shippingInfo,
            CancellationToken requestAborted)
        {
            if (_cart.LineItems.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            var formCollection = await HttpContext.Request.ReadFormAsync();
            if (ModelState.IsValid)
            {
                shippingInfo.LineItems = _cart.LineItems;

                await _repository.SaveOrderAsync(shippingInfo);

                var currentLink = "http://localhost:17352/";
                RequestInfo info = new RequestInfo();
                info.Merchant_id = merchantId;
                info.Merchant_password = merchantPassword;
                info.Receiver_email = merchantEmail;

                var money = shippingInfo.LineItems.Sum(e => e.Product.Price * e.Quanlity) * 20000;

                info.cur_code = "vnd";
                info.bank_code = shippingInfo.BankCode;
                info.Order_code = shippingInfo.ShippingInfoID + "dasdasdas";
                info.Total_amount = money + "";
                info.fee_shipping = "0";
                info.Discount_amount = "0";
                info.order_description = "Thanh toán đơn hàng";
                info.return_url = currentLink + "xac-nhan-don-hang.html";
                info.cancel_url = currentLink + "huy-don-hang.html";

                info.Buyer_fullname = shippingInfo.Name;
                info.Buyer_email = shippingInfo.Email;
                info.Buyer_mobile = shippingInfo.PhoneNumber;

                APICheckoutV3 objNLChecout = new APICheckoutV3();
                ResponseInfo result = await objNLChecout.GetUrlCheckoutAsync(info);
                //ResponseInfo result = await objNLChecout.GetUrlCheckoutAsync(info, order.PaymentMethod);
                if (result.Error_code == "00")
                {
                    return Redirect(result.Checkout_url);
                    //return Json(new
                    //{
                    //    status = true,
                    //    urlCheckout = result.Checkout_url,
                    //    message = result.Description
                    //});
                }
                else
                {
                    return Json(new
                    {
                        status = false,
                        message = result.Description,
                        result = result.Error_code
                    });
                }


                // Save all changes
                // await dbContext.SaveChangesAsync(requestAborted);


                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(shippingInfo);
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
