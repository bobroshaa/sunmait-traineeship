﻿using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface ICartService
{
    Task<List<CartItemViewModel>> GetUserCart(int userId);
    Task<CartItemViewModel?> GetById(int id);
    Task<CartItemViewModel> Add(CartItemInputModel cartItemInputModel);
    Task<CartItemViewModel>  Update(int id, int count);
    Task<CartItemViewModel> Delete(int id);
    Task<List<CartItemViewModel>> DeleteExpiredCartItems();
}