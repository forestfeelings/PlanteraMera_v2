using PlanteraMera_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanteraMera_v2.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            Seeds = new List<CartItem>();
            User = new ApplicationUser();
        }

        public decimal TotalPrice { get; set; }
        public List<CartItem> Seeds { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class CartItem
    {
        public Seed Seed { get; set; }
        public int Amount { get; set; }
    }
}
