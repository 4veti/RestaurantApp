using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.ClientApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Client,Cashier")]
public class ClientController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ClientController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost(ApiRoutes.Order)]
    public async Task<IActionResult> CreateOrder(OrderDto dto)
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

        if (!string.IsNullOrEmpty(addResult))
        {
            return BadRequest(addResult);
        }

        return Created();
    }

    [HttpGet(ApiRoutes.Menu)]
    public async Task<IActionResult> GetMenu()
    {
        MenuDto menu = await _serviceManager.OrderService.GetMenuAsync();

        return Ok(menu);
    }
}
