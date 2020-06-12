using OrdersService.Data;
using OrdersService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Repositories
{
    /* Checking */
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public Order Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public bool Delete(int orderId)
        {
            try
            {
                var order = _context.Orders.Find(orderId);
                _context.Orders.Remove(order);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
