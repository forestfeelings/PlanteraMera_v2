using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanteraMera_v2.Models;
using PlanteraMera_v2.Services;
using PlanteraMera_v2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanteraMera_v2.Controllers
{
    public class CartController : Controller
    {
        private readonly ISeedService _seedService;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(ISeedService seedService, UserManager<IdentityUser> userManager)
        {
            _seedService = seedService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            var cartIds = cart.Value.Split(',');
            var seeds = _seedService.GetAll();

            CartViewModel vm = new CartViewModel();
            vm.Seeds = new List<CartItem>();

            foreach (string id in cartIds)
            {
                var guid = Guid.Parse(id);

                if (vm.Seeds.Count > 0 && vm.Seeds.Any(s => s.Seed?.SeedId == guid))
                {
                    int seedIndex = vm.Seeds.FindIndex(s => s.Seed.SeedId == guid);
                    vm.Seeds[seedIndex].Amount += 1;
                }
                else
                {
                    var s = _seedService.GetSeedById(guid);

                    if (s != null)
                    {
                        vm.Seeds.Add(new CartItem() { Amount = 1, Seed = s });
                    }
                }
            }

            vm.CartTotal = vm.Seeds.Sum(s => s.Seed.Price * s.Amount);

            return View(vm);
        }

        [HttpPost]

        public IActionResult PlaceOrder([Bind("TotalPrice,Seeds")]CartViewModel vm)
        {
            Order order = new Order();

            order.TotalPrice = vm.CartTotal;
            order.OrderDate = DateTime.Now;
            order.UserId = Guid.Parse(_userManager.GetUserId(User));

            order.OrderRows = vm.Seeds.Select(cartItem => new OrderRow(cartItem)).ToList();

            return RedirectToAction("OrderSuccess", order);
        }

        public IActionResult OrderSuccess(Order order)
        {
            return View(order);
        }
    }
}
