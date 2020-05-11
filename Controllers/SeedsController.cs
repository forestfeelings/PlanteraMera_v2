using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanteraMera_v2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanteraMera_v2.Controllers
{
    public class SeedsController : Controller
    {
        private readonly ISeedService _seedService;

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
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            string cartContent = "";

            if (cart.Value != null)
            {
                cartContent = cart.Value;
                cartContent += "," + id;
            }
            else
            {
                cartContent += id;
            }

            Response.Cookies.Append("cart", cartContent);

            return RedirectToAction("Index");
        }
    }
}
