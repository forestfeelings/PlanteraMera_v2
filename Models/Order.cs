using PlanteraMera_v2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanteraMera_v2.Models
{
    public class Order
    {
        public Order()
        {
            OrderRows = new List<OrderRow>();
        }

        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderRow> OrderRows { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderRow
    {
        public OrderRow()
        {

        }
        public OrderRow(CartItem cartItem)
        {
            Amount = cartItem.Amount;
            Seed = cartItem.Seed;
        }

        public Seed Seed { get; set; }
        public int Amount { get; set; }
    }
}
