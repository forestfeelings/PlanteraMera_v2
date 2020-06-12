using SeedsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Models
{
    public class Order
    {
        //public Order()
        //{
        //    OrderRows = new List<OrderRow>();
        //}

        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        //public List<OrderRow> OrderRows { get; set; }
        public Guid UserId { get; set; }
        public Guid SeedId { get; set; }
        //public decimal TotalPrice { get; set; }
    }

    //public class OrderRow
    //{
    //    public Seed Seed { get; set; }
    //    public int Amount { get; set; }
    //}
}
