using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Services.Contracts;

namespace RestaurantApp.Services;

internal class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;

    public OrderService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<bool> AddAsync(OrderDto dto)
    {
        Order addOrder = new Order()
        {
            Created = DateTime.Now,
            Modified = DateTime.Now,
            OrderName = dto.OrderName,
            IsPaid = dto.IsPaid
        };

        await _repositoryManager.OrderRepository.InsertAsync(addOrder);
        bool addedOrder = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

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

        bool addedOrderItems = await _repositoryManager.UnitOfWork.SaveChangesAsync() > 0;

        return addedOrder && addedOrderItems;
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

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        Order? order = await _repositoryManager.OrderRepository.GetByIdAsync(id);
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

    public Task<bool> MarkCompletedAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> MarkPaidAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(int id, OrderDto dto)
    {
        Order? originalOrder = await _repositoryManager.OrderRepository.GetByIdAsync(id);

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
}
