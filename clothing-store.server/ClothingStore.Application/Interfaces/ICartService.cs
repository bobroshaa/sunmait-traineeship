using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;

namespace ClothingStore.Application.Interfaces;

public interface ICartService
{
    Task<List<CartItemViewModel>> GetUserCart(int userId);
    Task<CartItemViewModel?> GetById(int id);
    Task<CartItemPostResponseViewModel> Add(CartItemInputModel cartItemInputModel);
    Task Update(int id, int count);
    Task Delete(int id);
    Task<Dictionary<int, int>> DeleteExpiredCartItems();
}