using OrdersService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Repositories
{
    public interface IOrderRepository
    {
        public Order Create(Order order);

        public bool Delete(int orderId);
    }
}
