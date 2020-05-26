using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanteraMera_v2.Models;
using PlanteraMera_v2.Services;
using PlanteraMera_v2.ViewModels;

namespace PlanteraMera_v2.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class CartController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISeedService _seedService;

        private const string sessionKeyCart = "_cart";
        private const string sessionKeyUserId = "_userId";

        public CartController(ISeedService seedService, UserManager<ApplicationUser> userManager)
        {
            _seedService = seedService;
            _userManager = userManager;
        }

        [HttpGet]

        public JsonResult GetCartAmount()
        {
            var currentCartItems = HttpContext.Session.Get<List<CartItem>>(sessionKeyCart);

            if (currentCartItems == null)
            {
                return new JsonResult(0);
            }
            
            var totalItems = currentCartItems.Sum(x => x.Amount);


            return new JsonResult(totalItems);
        }

        [HttpGet]

        public IActionResult AddToCart(Guid id)
        {
            var currentCartItems = HttpContext.Session.Get<List<CartItem>>(sessionKeyCart);
            var sessionUserId = HttpContext.Session.Get<Guid>(sessionKeyUserId);
            var actualUserId = Guid.Parse(_userManager.GetUserId(User));

            List<CartItem> cartItems = new List<CartItem>();

            if (currentCartItems != null)
            {
                cartItems = currentCartItems;

                if (sessionUserId != actualUserId)
                {
                    currentCartItems = null;
                    HttpContext.Session.Clear();
                    cartItems = new List<CartItem>();
                }
            }

            HttpContext.Session.Set<Guid>(sessionKeyUserId, actualUserId);

            if (currentCartItems != null && currentCartItems.Any(x => x.Seed.SeedId == id))
            {
                int seedIndex = currentCartItems.FindIndex(x => x.Seed.SeedId == id);
                currentCartItems[seedIndex].Amount += 1;
                cartItems = currentCartItems;
            }
            else
            {
                var seed = _seedService.GetSeedById(id);
                CartItem newItem = new CartItem()
                {
                    Seed = seed,
                    Amount = 1
                };

                cartItems.Add(newItem);
            }

            HttpContext.Session.Set<List<CartItem>>(sessionKeyCart, cartItems);

            var totalItems = cartItems.Sum(x => x.Amount);

            return Ok(totalItems);
        }
    }
}