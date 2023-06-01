using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Interfaces;

public interface IUserRepository
{
    Task<List<UserAccount>> GetAll();
    Task<UserAccount?> GetById(int id);
    Task Add(UserAccount user);
    Task Update(UserAccount updatingUser, UserAccount user);
    Task Delete(UserAccount user);
    Task AddAddress(Address address);
    Task UpdateAddress(Address updatingAddress, Address address);
    Task UpdateRole(UserAccount user, Role role);
    Task<Address?> GetAddressByUserId(int userId);
    Task<bool> DoesEmailExist(string email);
    Task<bool> DoesPhoneNumberExist(string phone);
}