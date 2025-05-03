using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;
using static RestaurantApp.Domain.Constants;

namespace RestaurantApp.Services;

internal class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;

    public OrderService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<string> AddAsync(OrderDto dto)
    {
        try
        {
            List<string> validationErrors = await ValidateOrder(dto);

            if (validationErrors.Any())
            {
                return string.Join(" ", validationErrors);
            }

            Order addOrder = new Order()
            {
                Created = DateTime.Now,
                Modified = DateTime.Now,
                OrderName = dto.OrderName,
                IsPaid = dto.IsPaid
            };

            await _repositoryManager.OrderRepository.InsertAsync(addOrder);
            bool addedOrder = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

            if (addedOrder == false)
            {
                return ErrorMessages.DefaultError;
            }

            await _repositoryManager.FoodOrderRepository
                .InsertRangeAsync(dto.Foods.Select(f => new FoodOrder()
                {
                    FoodId = f.Id,
                    OrderId = addOrder.Id,
                    Count = f.Count
                }));

            await _repositoryManager.DrinkOrderRepository
                .InsertRangeAsync(dto.Drinks.Select(d => new DrinkOrder()
                {
                    DrinkId = d.Id,
                    OrderId = addOrder.Id,
                    Count = d.Count
                }));

            int expectedInsertCount = dto.Foods.Count() + dto.Drinks.Count();
            bool addedOrderItems = await _repositoryManager.UnitOfWork.SaveChangesAsync() == expectedInsertCount;

            if (addedOrderItems == false)
            {
                return string.Format(ErrorMessages.NotAllOrderItemsWereInserted, addOrder.Id);
            }

            return string.Empty;
        }
        catch (Exception ex) when (ex is DbUpdateException || ex is SqlException || ex is DbException)
        {
            return ErrorMessages.UnexpectedDatabaseError;
        }
        catch (Exception)
        {
            return ErrorMessages.DefaultError;
        }
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        Order? deleteOrder = await _repositoryManager.OrderRepository.GetByIdAsync(id);

        if (deleteOrder is not null)
        {
            _repositoryManager.OrderRepository.Remove(deleteOrder);
            bool isDeleted = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

            return isDeleted;
        }

        return false;
    }

    public async Task<IEnumerable<OrderDto>> GetAllByParamsAsync(OrderQueryParams queryParams)
    {
        List<OrderDto> orders = await _repositoryManager.OrderRepository
            .GetAllAsync(true)
            .Select(o => new OrderDto()
            {
                Created = o.Created,
                Completed = o.Completed,
                OrderName = o.OrderName,
                IsPaid = o.IsPaid,
                IsServed = o.IsServed
            })
            .ToListAsync();

        return orders;
    }

    public async Task<OrderDto?> GetByIdAsync(int orderId)
    {
        Order? order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);
        OrderDto? orderDto = null;

        if (order is not null)
        {
            orderDto = new OrderDto()
            {
                Created = order.Created,
                Completed = order.Completed,
                OrderName = order.OrderName,
                IsPaid = order.IsPaid,
                IsServed = order.IsServed
            };
        }

        return orderDto;
    }

    public async Task<bool> MarkCompletedAsync(int orderId)
    {
        Order? order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);

        if (order is null)
        {
            return false;
        }

        order.IsServed = true;
        order.Completed = DateTime.Now;

        bool isMarkedSuccessfully = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        return isMarkedSuccessfully;
    }

    public async Task<bool> MarkPaidAsync(int orderId)
    {
        Order? order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);

        if (order is null)
        {
            return false;
        }

        order.IsPaid = true;

        bool isMarkedSuccessfully = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        return isMarkedSuccessfully;
    }

    public async Task<bool> UpdateAsync(int orderId, OrderDto dto)
    {
        Order? originalOrder = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);

        if (originalOrder is null)
        {
            return false;
        }

        originalOrder.OrderName = dto.OrderName;
        originalOrder.Modified = DateTime.Now;

        _repositoryManager.OrderRepository.Update(originalOrder);
        bool successfulUpdate = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        return successfulUpdate;
    }

    public async Task<MenuDto> GetMenuAsync()
    {
        List<FoodDto> foods = await _repositoryManager.FoodRepository
            .GetAllAsync()
            .Select(f => new FoodDto()
            {
                Id = f.Id,
                Name = f.Name,
                NetGrams = f.NetGrams,
                Price = f.Price,
                FoodTypeId = f.FoodTypeId,
                FoodTypeName = f.FoodType.Name
            }).ToListAsync();

        List<DrinkDto> drinks = await _repositoryManager.DrinkRepository
            .GetAllAsync()
            .Select(d => new DrinkDto()
            {
                Id = d.Id,
                Name = d.Name,
                Price = d.Price,
                DrinkTypeId = d.DrinkTypeId,
                DrinkType = d.DrinkType.Name,
                Millilitres = d.Millilitres,
                IsAlcoholic = d.IsAlcoholic,
                AlcoholPercentage = d.AlcoholPercentage,
            }).ToListAsync();

        List<FoodTypeDto> foodTypes = await _repositoryManager.FoodTypeRepository
            .GetAllAsync()
            .Select(ft => new FoodTypeDto()
            {
                Id = ft.Id,
                Name = ft.Name
            }).ToListAsync();

        List<DrinkTypeDto> drinkTypes = await _repositoryManager.DrinkTypeRepository
            .GetAllAsync()
            .Select(dt => new DrinkTypeDto()
            {
                Id = dt.Id,
                Name = dt.Name
            }).ToListAsync();

        MenuDto menu = new MenuDto()
        {
            Foods = foods,
            Drinks = drinks,
            FoodTypes = foodTypes,
            DrinkTypes = drinkTypes
        };

        return menu;
    }

    private async Task<List<string>> ValidateOrder(OrderDto dto)
    {
        List<string> errors = new List<string>();

        try
        {
            if (dto.OrderName.Length < OrderNameMinLength || dto.OrderName.Length > OrderNameMaxLength)
            {
                errors.Add(string.Format(ErrorMessages.InvalidNameLength, OrderNameMinLength, OrderNameMaxLength));
            }

            bool falseVariable = false;
            List<int>? invalidFoodIDs = null;

            if (dto.Foods.Any())
            {
                invalidFoodIDs = await _repositoryManager.FoodRepository
                    .GetAllAsync()
                    .Select(f => f.Id)
                    .Where(f => dto.Foods.Select(x => x.Id).Contains(f) == falseVariable)
                    .ToListAsync();
            }

            if (invalidFoodIDs?.Any() ?? false)
            {
                errors.Add(string.Format(ErrorMessages.InvalidFoodIDs, string.Join(", ", invalidFoodIDs)));
            }

            List<int>? invalidDrinkIDs = null;

            if (dto.Drinks.Any())
            {
                invalidDrinkIDs = await _repositoryManager.DrinkRepository
                .GetAllAsync()
                .Select(d => d.Id)
                .Where(d => dto.Foods.Select(x => x.Id).Contains(d) == falseVariable)
                .ToListAsync();
            }

            if (invalidDrinkIDs?.Any() ?? false)
            {
                errors.Add(string.Format(ErrorMessages.InvalidDrinkIDs, string.Join(", ", invalidDrinkIDs)));
            }

            return errors;
        }
        catch (Exception)
        {
            errors.Add(ErrorMessages.UnexpectedValidationError);
            return errors;
        }
    }
}
