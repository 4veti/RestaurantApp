using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.ClientApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrontDeskController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public FrontDeskController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("Food")]
        public async Task<IActionResult> AddFood([FromBody] FoodDto dto)
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

        [HttpPut("Food")]
        public async Task<IActionResult> UpdateFood([FromBody] FoodDto dto)
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

                string? updateResult = await _serviceManager.FoodService.UpdateAsync(dto);

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

        [HttpDelete("Food")]
        public async Task<IActionResult> DeleteFood([FromBody] int foodId)
        {
            try
            {
                string? deletedResult = await _serviceManager.FoodService.DeleteByIdAsync(foodId);

                if (deletedResult is null)
                {
                    return NotFound();
                }
                else if (string.IsNullOrEmpty(deletedResult) == false)
                {
                    return BadRequest(deletedResult);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("Drink")]
        public async Task<IActionResult> AddDrink([FromBody] DrinkDto dto)
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

        [HttpPut("Drink")]
        public async Task<IActionResult> UpdateDrink([FromBody] DrinkDto dto)
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

                string? updateResult = await _serviceManager.DrinkService.UpdateAsync(dto);

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

        [HttpDelete("Drink")]
        public async Task<IActionResult> DeleteDrink([FromBody] int drinkId)
        {
            try
            {
                string? deletedResult = await _serviceManager.DrinkService.DeleteByIdAsync(drinkId);

                if (deletedResult is null)
                {
                    return NotFound();
                }
                else if (string.IsNullOrEmpty(deletedResult) == false)
                {
                    return BadRequest(deletedResult);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("FoodType")]
        public async Task<IActionResult> AddFoodType([FromBody] FoodTypeDto dto)
        {
            try
            {
                if (dto is null)
                {
                    return BadRequest();
                }

                string result = await _serviceManager.FoodTypeService.AddAsync(dto);

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

        [HttpPut("FoodType")]
        public async Task<IActionResult> UpdateFoodType([FromBody] FoodTypeDto dto)
        {
            try
            {
                if (dto is null)
                {
                    return BadRequest();
                }

                string? updateResult = await _serviceManager.FoodTypeService.UpdateAsync(dto);

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

        [HttpDelete("FoodType")]
        public async Task<IActionResult> DeleteFoodType([FromBody] int foodTypeId)
        {
            try
            {
                string? deleteResult = await _serviceManager.FoodTypeService.DeleteByIdAsync(foodTypeId);

                if (deleteResult is null)
                {
                    return NotFound();
                }
                if (string.IsNullOrEmpty(deleteResult) == false)
                {
                    return BadRequest(deleteResult);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("DrinkType")]
        public async Task<IActionResult> AddDrinkType([FromBody] DrinkTypeDto dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            string insertResult = await _serviceManager.DrinkTypeService.AddAsync(dto);

            if (string.IsNullOrEmpty(insertResult) == false)
            {
                return BadRequest(insertResult);
            }

            return Created();
        }

        [HttpPut("DrinkType")]
        public async Task<IActionResult> UpdateDrinkType([FromBody] DrinkTypeDto dto)
        {
            if (dto is null)
            {
                return BadRequest();
            }

            string? updateResult = await _serviceManager.DrinkTypeService.UpdateAsync(dto);

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

        [HttpDelete("DrinkType")]
        public async Task<IActionResult> DeleteDrinkType(int drinkTypeId)
        {
            string? deleteResult = await _serviceManager.DrinkTypeService.DeleteByIdAsync(drinkTypeId);

            if (deleteResult is null)
            {
                return NotFound();
            }
            else if (string.IsNullOrEmpty(deleteResult) == false)
            {
                return BadRequest(deleteResult);
            }

            return NoContent();
        }
    }
}
