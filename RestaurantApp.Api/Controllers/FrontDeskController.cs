using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Domain;
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

        [HttpPost(ApiRoutes.Food)]
        public async Task<IActionResult> AddFood([FromBody] FoodDto dto)
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

        [HttpPut(ApiRoutes.Food)]
        public async Task<IActionResult> UpdateFood([FromBody] FoodDto dto)
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

        [HttpDelete(ApiRoutes.Food)]
        public async Task<IActionResult> DeleteFood([FromBody] int foodId)
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

        [HttpPost(ApiRoutes.Drink)]
        public async Task<IActionResult> AddDrink([FromBody] DrinkDto dto)
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

        [HttpPut(ApiRoutes.Drink)]
        public async Task<IActionResult> UpdateDrink([FromBody] DrinkDto dto)
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

        [HttpDelete(ApiRoutes.Drink)]
        public async Task<IActionResult> DeleteDrink([FromBody] int drinkId)
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

        [HttpGet(ApiRoutes.FoodType)]
        public async Task<IActionResult> GetFoodTypes()
        {
            IEnumerable<FoodTypeDto> foodTypes = await _serviceManager.FoodTypeService.GetAllAsync();

            return Ok(foodTypes);
        }

        [HttpPost(ApiRoutes.FoodType)]
        public async Task<IActionResult> AddFoodType([FromBody] FoodTypeDto dto)
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

        [HttpPut(ApiRoutes.FoodType)]
        public async Task<IActionResult> UpdateFoodType([FromBody] FoodTypeDto dto)
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

        [HttpDelete(ApiRoutes.FoodType)]
        public async Task<IActionResult> DeleteFoodType([FromBody] int foodTypeId)
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

        [HttpGet(ApiRoutes.DrinkType)]
        public async Task<IActionResult> GetDrinkTypes()
        {
            IEnumerable<DrinkTypeDto> drinkTypes = await _serviceManager.DrinkTypeService.GetAllAsync();

            return Ok(drinkTypes);
        }

        [HttpPost(ApiRoutes.DrinkType)]
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

        [HttpPut(ApiRoutes.DrinkType)]
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

        [HttpDelete(ApiRoutes.DrinkType)]
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

        [HttpPut(ApiRoutes.OrderPaid)]
        public async Task<IActionResult> MarkOrderAsPaid([FromBody] int orderId)
        {
            if (await _serviceManager.OrderService.MarkPaidAsync(orderId) == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet(ApiRoutes.Orders)]
        public async Task<IActionResult> GeNewOrders([FromQuery] int lastOrderId, [FromQuery] bool? isPaid)
        {
            OrderQueryParams queryParams = new OrderQueryParams()
            {
                LastOrderId = lastOrderId,
                IsPaid = isPaid
            };

            IEnumerable<OrderDto> orders = await _serviceManager.OrderService.GetAllByParamsAsync(queryParams);

            return Ok(orders);
        }
    }
}
