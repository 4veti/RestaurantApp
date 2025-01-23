using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services;

namespace RestaurantApp.ClientApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrontDeskController : Controller
    {
        private readonly ServiceManager _serviceManager;

        public FrontDeskController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> Food(FoodDto dto)
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

                string result = await _serviceManager.FoodService.AddAsync(dto);

                if (string.IsNullOrEmpty(result) == false)
                {
                    return BadRequest(result);
                }

                return Created();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Food(int foodId, FoodDto dto)
        {
            try
            {
                if (dto is null)
                {
                    return BadRequest();
                }
                
                if (foodId < 1)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                string? updateResult = await _serviceManager.FoodService.UpdateAsync(foodId, dto);

                if (updateResult is null)
                {
                    return NotFound();
                }
                else if (string.IsNullOrEmpty(updateResult) == false)
                {
                    return BadRequest(updateResult);
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Food(int foodId)
        {
            try
            {
                if (foodId < 1)
                {
                    return BadRequest();
                }

                bool? isDeleted = await _serviceManager.FoodService.DeleteByIdAsync(foodId);

                if (isDeleted is null)
                {
                    return NotFound();
                }
                else if (isDeleted == false)
                {
                    return StatusCode(500);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Drink(DrinkDto dto)
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

                string result = await _serviceManager.DrinkService.AddAsync(dto);

                if (string.IsNullOrEmpty(result) == false)
                {
                    return BadRequest(result);
                }

                return Created();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Drink(int drinkId, DrinkDto dto)
        {
            try
            {
                if (dto is null)
                {
                    return BadRequest();
                }

                if (drinkId < 1)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                string? updateResult = await _serviceManager.DrinkService.UpdateAsync(drinkId, dto);

                if (updateResult is null)
                {
                    return NotFound();
                }
                else if (string.IsNullOrEmpty(updateResult) == false)
                {
                    return BadRequest(updateResult);
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
