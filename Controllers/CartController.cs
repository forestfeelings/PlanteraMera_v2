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

        private const string sessionKeyCart = "_cart";
        private const string sessionKeyUserId = "_userId";

        public CartController(ISeedService seedService, UserManager<ApplicationUser> userManager)
        {
            _seedService = seedService;
            _userManager = userManager;
        }

        /* Kollar om sessionen är aktiv och om det finns varor tillagda i sessionen */

        public IActionResult Index()
        {
            var cartUserId = HttpContext.Session.Get<Guid>(sessionKeyUserId);

            CartViewModel vm = new CartViewModel();

            if (cartUserId == null || cartUserId == Guid.Empty)
            {
                ViewBag.Message = "Sessionen har gått ut!";
                return View(vm);
            }

            var cart = HttpContext.Session.Get<List<CartItem>>(sessionKeyCart);
            var seeds = _seedService.GetAll();

            vm.Seeds = cart;

            vm.TotalPrice = vm.Seeds.Sum(s => s.Seed.Price * s.Amount);
            
            return View(vm);
        }

        /* Postar en order med information om varor och användare */

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([Bind("TotalPrice,Seeds")]CartViewModel cart)
        {
            OrderViewModel vm = new OrderViewModel();

            Order order = new Order();

            order.TotalPrice = cart.TotalPrice;
            order.OrderDate = DateTime.Now;
            order.UserId = Guid.Parse(_userManager.GetUserId(User));

            order.OrderRows = cart.Seeds.Select(cartItem => new OrderRow(cartItem)).ToList();

            vm.Order = order;

            var user = await _userManager.GetUserAsync(User);

            vm.User = user;

            return View("OrderSuccess", vm);
        }
    }
}