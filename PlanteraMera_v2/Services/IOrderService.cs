using PlanteraMera_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanteraMera_v2.Services
{
    public interface IOrderService
    {
        public Task<bool> PlaceOrder(Order order);
    }
}
