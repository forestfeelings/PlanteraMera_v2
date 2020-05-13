using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanteraMera_v2.Models;
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
        private readonly UserManager<ApplicationUser> _userManager;

        private const string sessionKeyCart = "_cart";
        private const string sessionKeyUserId = "_userId";

        public SeedsController(ISeedService seedService, UserManager<ApplicationUser> userManager)
        {
            _seedService = seedService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var seeds = _seedService.GetAll();

            return View(seeds);
        }

        /* Skickar de klickade varorna till sessions efter authorisation av inloggad användare */

        [Authorize]
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