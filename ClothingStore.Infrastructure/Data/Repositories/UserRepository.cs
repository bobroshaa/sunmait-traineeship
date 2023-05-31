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
        return await _dbContext.Users.Where(u => u.IsActive).ToListAsync();
    }

    public async Task<UserAccount?> GetById(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.ID == id && u.IsActive);
    }

    public async Task Add(UserAccount userAccount)
    {
        await _dbContext.Users.AddAsync(userAccount);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(UserAccount updatingUser, UserAccount user)
    {
        updatingUser.Phone = user.Phone;
        updatingUser.Email = user.Email;
        updatingUser.FirstName = user.FirstName;
        updatingUser.LastName = user.LastName;

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(UserAccount user)
    {
        user.IsActive = false;
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAddress(Address? address)
    {
        await _dbContext.Addresses.AddAsync(address);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAddress(Address updatingAddress, Address address)
    {
        updatingAddress.Country = address.Country;
        updatingAddress.District = address.District;
        updatingAddress.City = address.City;
        updatingAddress.Postcode = address.Postcode;
        updatingAddress.AddressLine1 = address.AddressLine1;
        updatingAddress.AddressLine2 = address.AddressLine2;

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRole(UserAccount user, Role role)
    {
        user.Role = role;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Address?> GetAddressByUserId(int userId)
    {
        return await _dbContext.Addresses.FirstOrDefaultAsync(a => a.UserID == userId && a.IsActive);
    }

    public async Task<bool> EmailIsUnique(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsActive) is null;
    }

    public async Task<bool> PhoneIsUnique(string phone)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Phone == phone && u.IsActive) is null;
    }
}