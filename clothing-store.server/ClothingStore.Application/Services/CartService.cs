using AutoMapper;
using ClothingStore.Application.Exceptions;
using ClothingStore.Application.Interfaces;
using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Application.Options;
using ClothingStore.Domain.Entities;
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


    public async Task<CartItemViewModel> Add(CartItemInputModel cartItemInputModel)
    {
        var product = await GetProductById(cartItemInputModel.ProductID);
        await ValidateUser(cartItemInputModel.UserID);
        ValidateQuantity(cartItemInputModel, product);

        var cartItem = _mapper.Map<CartItem>(cartItemInputModel);

        var existingCartItem =
            await GetCartItemByUserAndProduct(cartItemInputModel.UserID, cartItemInputModel.ProductID);
        if (existingCartItem is null)
        {
            cartItem.ReservationEndDate =
                DateTime.UtcNow + TimeSpan.FromSeconds(_reservationConfiguration.reservationTime);

            _cartRepository.Add(cartItem);

            cartItem.Product = product;
        }
        else
        {
            existingCartItem.Quantity += cartItem.Quantity;
        }

        product.ReservedQuantity += cartItem.Quantity;

        var cartItemVm = existingCartItem is null
            ? _mapper.Map<CartItemViewModel>(cartItem)
            : _mapper.Map<CartItemViewModel>(existingCartItem);

        await _cartRepository.SaveChanges();

        return cartItemVm;
    }

    public async Task<CartItemViewModel> Update(int id, int count)
    {
        var cartItem = await GetCartItemById(id);

        cartItem.Product.ReservedQuantity += count - cartItem.Quantity;
        cartItem.Quantity = count;

        await _cartRepository.SaveChanges();

        var cartItemVm = _mapper.Map<CartItemViewModel>(cartItem);

        return cartItemVm;
    }

    public async Task<CartItemViewModel> Delete(int id)
    {
        var cartItem = await GetCartItemById(id);
        var product = await GetProductById(cartItem.ProductID);

        _cartRepository.Delete(cartItem);

        product.ReservedQuantity -= cartItem.Quantity;

        await _cartRepository.SaveChanges();

        var cartItemVm = _mapper.Map<CartItemViewModel>(cartItem);

        return cartItemVm;
    }

    private async Task ValidateUser(int id)
    {
        if (!await _userRepository.DoesUserExist(id))
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.UserNotFound, id));
        }
    }

    private void ValidateQuantity(CartItemInputModel cartItemInputModel, Product product)
    {
        if (cartItemInputModel.Quantity > product.InStockQuantity - product.ReservedQuantity)
        {
            throw new IncorrectParamsException(string.Format(
                ExceptionMessages.ProductQuantityIsNotAvailable,
                cartItemInputModel.Quantity, product.ID,
                product.InStockQuantity - product.ReservedQuantity));
        }
    }

    private async Task<Product> GetProductById(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product is null)
        {
            throw new EntityNotFoundException(string.Format(ExceptionMessages.ProductNotFound, id));
        }

        return product;
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

    private async Task<CartItem?> GetCartItemByUserAndProduct(int userId, int productId)
    {
        var cartItem = await _cartRepository.GetByUserAndProduct(userId, productId);

        return cartItem;
    }

    public async Task<List<CartItemViewModel>> DeleteExpiredCartItems()
    {
        var deletedCartItems = await _cartRepository.DeleteExpired();

        await _cartRepository.SaveChanges();

        Console.WriteLine("DeleteExpiredCartItems:" + DateTime.Now);

        var deletedCartItemVms = _mapper.Map<List<CartItemViewModel>>(deletedCartItems);

        return deletedCartItemVms;
    }
}