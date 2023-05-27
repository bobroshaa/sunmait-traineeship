using AutoMapper;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Interfaces;

namespace ClothingStore.Application.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public OrderItemService(IMapper mapper, IOrderItemRepository orderItemRepository)
    {
        _mapper = mapper;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<List<OrderItemViewModel>> GetAllById(int id)
    {
        return _mapper.Map<List<OrderItemViewModel>>(await _orderItemRepository.GetAllById(id));
    }


    public async Task<OrderItemViewModel?> GetById(int id)
    {
        var orderItem = await _orderItemRepository.GetById(id);
        if (orderItem is null)
        {
            throw new Exception(ExceptionMessages.OrderItemNotFound);
        }

        return _mapper.Map<OrderItemViewModel?>(orderItem);
    }
    
    public async Task<int> Add(OrderItemInputModel orderItemInputModel)
    {
        var orderItem = _mapper.Map<OrderProduct>(orderItemInputModel);
        await _orderItemRepository.Add(orderItem);
        return orderItem.ID;
    }

    public async Task Update(int id, OrderItemInputModel orderItemInputModel)
    {
        var updatingBrand = await _orderItemRepository.GetById(id);
        if (updatingBrand is null)
        {
            throw new Exception(ExceptionMessages.OrderItemNotFound);
        }

        await _orderItemRepository.Update(updatingBrand, _mapper.Map<OrderProduct>(orderItemInputModel));
    }

    public async Task Delete(int id)
    {
        var brand = await _orderItemRepository.GetById(id);
        if (brand is null)
        {
            throw new Exception(ExceptionMessages.OrderItemNotFound);
        }

        await _orderItemRepository.Delete(brand);
    }
}