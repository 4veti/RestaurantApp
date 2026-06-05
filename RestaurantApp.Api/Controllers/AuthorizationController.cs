using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.ClientApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AuthorizationController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost(ApiRoutes.Login)]
    public async Task<IActionResult> Login(LoginRequestDTO request)
    {
        (bool isAuthenticationSuccessful, LoginResponseDTO? response) = await _serviceManager.AuthorizationService.Login(request);

        if (isAuthenticationSuccessful == false)
        {
            return Unauthorized();
        }

        return Ok(response);
    }

    [HttpPost(ApiRoutes.RefreshAccessToken)]
    public async Task<IActionResult> RefreshAccessToken(RefreshAccessTokenRequestDTO request)
    {
        (bool isAuthenticationSuccessful, LoginResponseDTO? response) = await _serviceManager.AuthorizationService.RefreshAccessToken(request);

        if (isAuthenticationSuccessful == false)
        {
            return Unauthorized();
        }

        return Ok(response);
    }
}
