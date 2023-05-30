using ClothingStore.Application.Models.InputModels;
using ClothingStore.Application.Models.ViewModels;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Interfaces;

public interface IUserService
{
    Task<List<UserViewModel>> GetAll();
    Task<UserViewModel?> GetById(int id);
    public Task<int> Add(UserInputModel userInputModel);
    public Task Update(int id, UserInputModel user);
    Task Delete(int id);
    Task UpdateAddress(int userId, AddressInputModel address);
    Task UpdateRole(int id, Role role);
}