using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KitchenController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public KitchenController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Route("AnyOrders")]
        public async Task<IActionResult> AnyOrders([FromQuery] int lastOrderId)
        {
            OrderQueryParams queryParams = new OrderQueryParams()
            {
                LastOrderId = lastOrderId,
                IsPaid = true,
                OnlyNotServed = true
            };

            bool anyOrders = await _serviceManager.OrderService.GetAllCountByParamsAsync(queryParams);

            return Ok(anyOrders);
        }

        [HttpGet]
        [Route("Orders")]
        public async Task<IActionResult> GetOrders([FromQuery] int lastOrderId)
        {
            OrderQueryParams queryParams = new OrderQueryParams()
            {
                LastOrderId = lastOrderId,
                IsPaid = true
            };

            IEnumerable<OrderDto> orders = await _serviceManager.OrderService.GetAllByParamsAsync(queryParams);

            return Ok(orders);
        }

        [HttpPost]
        [Route("OrderServed")]
        public async Task<IActionResult> MarkOrderAsServed([FromBody] int orderId)
        {
            if (await _serviceManager.OrderService.MarkServedAsync(orderId) == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
