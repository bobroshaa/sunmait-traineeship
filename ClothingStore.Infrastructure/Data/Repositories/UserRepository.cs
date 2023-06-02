using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly Context _dbContext;

    public UserRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserAccount>> GetAll()
    {
        return await _dbContext.Users
            .Where(u => u.IsActive)
            .Include(u => u.Address)
            .ToListAsync();
    }

    public async Task<UserAccount?> GetById(int id)
    {
        return await _dbContext.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.ID == id && u.IsActive);
    }

    public void Add(UserAccount userAccount)
    {
        _dbContext.Users.Add(userAccount);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Delete(UserAccount user)
    {
        user.IsActive = false;
    }

    public void AddAddress(Address address)
    {
        _dbContext.Addresses.Add(address);
    }

    public void UpdateRole(UserAccount user, Role role)
    {
        user.Role = role;
    }

    public async Task<Address?> GetAddressByUserId(int userId)
    {
        return await _dbContext.Addresses.FirstOrDefaultAsync(a => a.UserID == userId && a.IsActive);
    }

    public async Task<bool> DoesEmailExist(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email && u.IsActive);
    }

    public async Task<bool> DoesPhoneNumberExist(string phone)
    {
        return await _dbContext.Users.AnyAsync(u => u.Phone == phone && u.IsActive);
    }
}