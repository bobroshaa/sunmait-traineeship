using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Interfaces;

public interface IUserRepository
{
    Task<List<UserAccount>> GetAll();
    Task<UserAccount?> GetById(int id);
    void Add(UserAccount user);
    Task Save();
    void Delete(UserAccount user);
    void AddAddress(Address address);
    void UpdateRole(UserAccount user, Role role);
    Task<Address?> GetAddressByUserId(int userId);
    Task<bool> DoesEmailExist(string email);
    Task<bool> DoesPhoneNumberExist(string phone);
    Task<bool> DoesUserExist(int id);
}