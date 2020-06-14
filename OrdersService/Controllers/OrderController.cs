using Microsoft.AspNetCore.Mvc;
using OrdersService.Filters;
using OrdersService.Models;
using OrdersService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]

        public ActionResult<Order> Create([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            var createdOrder = _orderRepository.Create(order);

            return Ok(createdOrder);
        }

        [HttpDelete]

        public ActionResult<int> Delete(int id)
        {
            var wasDeleted = _orderRepository.Delete(id);

            if (wasDeleted)
            {
                return Ok(id);
            }
            else
            {
                return NotFound(id);
            }
        }

        [HttpGet]

        public ActionResult<string> Secret()
        {
            return Ok("Detta är ännu en hemlis...");
        }
    }
}
