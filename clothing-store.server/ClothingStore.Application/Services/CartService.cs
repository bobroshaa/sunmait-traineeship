using AutoMapper;
using ClothingStore.Application.Exceptions;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Application.Options;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace ClothingStore.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ReservationConfiguration _reservationConfiguration;

    public CartService(
        IMapper mapper,
        ICartRepository cartRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        IOptions<ReservationConfiguration> options)
    {
        _mapper = mapper;
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _reservationConfiguration = options.Value;
    }

    public async Task<List<CartItemViewModel>> GetUserCart(int userId)
    {
        await ValidateUser(userId);

        var cartItems = await _cartRepository.GetUserCart(userId);
        var cartItemVms = _mapper.Map<List<CartItemViewModel>>(cartItems);

        return cartItemVms;
    }

    public async Task<CartItemViewModel?> GetById(int id)
    {
        var cartItem = await GetCartItemById(id);
        var cartItemVm = _mapper.Map<CartItemViewModel>(cartItem);

        return cartItemVm;
    }

    public async Task<CartItemPostResponseViewModel> Add(CartItemInputModel cartItemInputModel)
    {
        await ValidateProduct(cartItemInputModel.ProductID);
        await ValidateUser(cartItemInputModel.UserID);

        var cartItem = _mapper.Map<CartItem>(cartItemInputModel);

        cartItem.Status = CartItemStatus.Reserved;

        _cartRepository.Add(cartItem);

        await _cartRepository.SaveChanges();

        var response = new CartItemPostResponseViewModel
            { Id = cartItem.ID, ReservationTime = _reservationConfiguration.reservationTime };

        return response;
    }

    public async Task Update(int id, int count)
    {
        var cartItem = await GetCartItemById(id);

        cartItem.Quantity = count;
        await _cartRepository.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var cartItem = await GetCartItemById(id);

        _cartRepository.Delete(cartItem);

        await _cartRepository.SaveChanges();
    }

    private async Task ValidateUser(int id)
    {
        if (!await _userRepository.DoesUserExist(id))
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.UserNotFound, id));
        }
    }

    private async Task ValidateProduct(int id)
    {
        if (!await _productRepository.DoesProductExist(id))
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, id));
        }
    }

    private async Task<CartItem> GetCartItemById(int id)
    {
        var cartItem = await _cartRepository.GetById(id);
        if (cartItem is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.CartItemNotFound, id));
        }

        return cartItem;
    }
}