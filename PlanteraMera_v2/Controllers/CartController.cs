using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanteraMera_v2.Models;
using PlanteraMera_v2.Services;
using PlanteraMera_v2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanteraMera_v2.Controllers
{
    public class CartController : Controller
    {
        private readonly ISeedService _seedService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;

        private const string sessionKeyCart = "_cart";
        private const string sessionKeyUserId = "_userId";

        public CartController(ISeedService seedService, UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            _seedService = seedService;
            _userManager = userManager;
            _orderService = orderService;
        }

        /* Kollar om sessionen är aktiv och om det finns varor tillagda i sessionen */

        public async Task<IActionResult> Index()
        {
            var cartUserId = HttpContext.Session.Get<Guid>(sessionKeyUserId);

            CartViewModel vm = new CartViewModel();

            var user = await _userManager.GetUserAsync(User);

            vm.User = user;

            if (cartUserId == null || cartUserId == Guid.Empty)
            {
                ViewBag.Message = "Sessionen har gått ut!";
                return View(vm);
            }

            var cart = HttpContext.Session.Get<List<CartItem>>(sessionKeyCart);
            var seeds = await _seedService.GetAll();
            
            vm.Seeds = cart;

            vm.TotalPrice = vm.Seeds.Sum(s => s.Seed.Price * s.Amount);

            return View(vm);
        }

        /* Postar en order med information om varor och användare */

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([Bind("TotalPrice,Seeds")]CartViewModel cart)
        {
            OrderViewModel vm = new OrderViewModel();

            var seedList = cart.Seeds.Select(x => x.Seed).ToList();

            //var orderRowList = cart.Seeds.Select(cartItem => new OrderRow(cartItem)).ToList();

            var orderId = Guid.NewGuid();

            Order order = new Order();

            foreach (var seed in seedList)
            {
                order.OrderId = orderId;
                order.SeedId = seed.SeedId;
                order.UserId = Guid.Parse(_userManager.GetUserId(User));
                order.OrderDate = DateTime.Now;
            }

            var orderIsPlaced = await _orderService.PlaceOrder(order);

            if (orderIsPlaced)
            {
                vm.Order = order;
                vm.Order.TotalPrice = cart.TotalPrice;
                vm.Order.OrderRows = cart.Seeds.Select(cartItem => new OrderRow(cartItem)).ToList();

                var user = await _userManager.GetUserAsync(User);

                vm.User = user;

                return View("OrderSuccess", vm);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}