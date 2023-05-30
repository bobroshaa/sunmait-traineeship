using ClothingStore.Domain.Entities;

namespace ClothingStore.Domain.Interfaces;

public interface IUserRepository
{
    Task<List<UserAccount>> GetAll();
    Task<UserAccount?> GetById(int id);
    Task Add(UserAccount user);
    Task Update(UserAccount updatingUser, UserAccount user);
    Task Delete(UserAccount user);
}