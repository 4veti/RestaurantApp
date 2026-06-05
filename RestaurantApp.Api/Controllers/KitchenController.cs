using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.ClientApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Kitchen")]
public class KitchenController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public KitchenController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    [Route(ApiRoutes.AnyOrders)]
    public async Task<IActionResult> AnyOrders([FromQuery] int lastOrderId)
    {
        OrderQueryParams queryParams = new OrderQueryParams()
        {
            LastOrderId = lastOrderId,
            IsPaid = true,
            OnlyNotServed = true,
            FromDate = DateTime.Today,
            MustHaveFoods = true
        };

        bool anyOrders = await _serviceManager.OrderService.GetAllCountByParamsAsync(queryParams);

        return Ok(anyOrders);
    }

    [HttpGet]
    [Route(ApiRoutes.Orders)]
    public async Task<IActionResult> GetOrders([FromQuery] int lastOrderId)
    {
        OrderQueryParams queryParams = new OrderQueryParams()
        {
            LastOrderId = lastOrderId,
            IsPaid = true,
            OnlyNotServed = true,
            FromDate = DateTime.Today,
            MustHaveFoods = true
        };

        IEnumerable<OrderDto> orders = await _serviceManager.OrderService.GetAllByParamsAsync(queryParams);

        return Ok(orders);
    }

    [HttpPut]
    [Route(ApiRoutes.OrderServed)]
    public async Task<IActionResult> MarkOrderAsServed([FromBody] int orderId)
    {
        if (await _serviceManager.OrderService.MarkServedAsync(orderId) == false)
        {
            return BadRequest();
        }

        return Ok();
    }
}
