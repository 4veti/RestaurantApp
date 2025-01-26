using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.ClientApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ClientController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("Order")]
        public async Task<IActionResult> CreateOrder(OrderDto dto)
        {
            try
            {
                if (dto is null)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                string addResult = await _serviceManager.OrderService.AddAsync(dto);

                if (string.IsNullOrEmpty(addResult))
                {
                    return BadRequest(addResult);
                }

                return Created();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
