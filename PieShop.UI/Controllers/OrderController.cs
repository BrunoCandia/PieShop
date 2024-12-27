﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PieShop.BusinessLogic;
using PieShop.Models.Order;
using PieShop.UI.Models;

namespace PieShop.UI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderController(IOrderService orderService, IShoppingCartService shoppingCartService)
        {
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
        }

        //It is Get by default
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderCheckoutViewModel orderCheckoutViewModel)
        {
            var items = await _shoppingCartService.GetShoppingCartItemsAsync();

            if (items.Count == 0)
            {
                ModelState.AddModelError("emptyCart", "Your cart is empty.");
            }

            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    FirstName = orderCheckoutViewModel.Order.FirstName,
                    LastName = orderCheckoutViewModel.Order.LastName,
                    AddressLine1 = orderCheckoutViewModel.Order.AddressLine1,
                    AddressLine2 = orderCheckoutViewModel.Order.AddressLine2,
                    ZipCode = orderCheckoutViewModel.Order.ZipCode,
                    City = orderCheckoutViewModel.Order.City,
                    State = orderCheckoutViewModel.Order.State,
                    Country = orderCheckoutViewModel.Order.Country,
                    PhoneNumber = orderCheckoutViewModel.Order.PhoneNumber,
                    Email = orderCheckoutViewModel.Order.Email
                };

                await _orderService.CreateOrderAsync(order);
                await _shoppingCartService.ClearCartAsync();

                return RedirectToAction("CheckoutCompleted");
            }

            return View(orderCheckoutViewModel);
        }

        public IActionResult CheckoutCompleted()
        {
            ViewBag.CheckoutCompletedMessage = "Thanks for your order. You'll soon enjoy our delicious pies!";

            return View();
        }
    }
}