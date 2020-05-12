using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanteraMera_v2.Services;
using PlanteraMera_v2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlanteraMera_v2.Controllers
{
    public class SeedsController : Controller
    {
        private readonly ISeedService _seedService;

        private const string sessionKeyCart = "_cart";

        public SeedsController(ISeedService seedService)
        {
            _seedService = seedService;
        }

        public IActionResult Index()
        {
            var seeds = _seedService.GetAll();

            return View(seeds);
        }

        [Authorize]
        public IActionResult AddToCart(Guid id)
        {
            var currentCartItems = HttpContext.Session.Get<List<CartItem>>(sessionKeyCart);
            
            List<CartItem> cartItems = new List<CartItem>();

            if (currentCartItems != null)
            {
                cartItems = currentCartItems;
            }

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

            return RedirectToAction("Index");
        }
    }
}

public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}
