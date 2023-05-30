using ClothingStore.Domain.Entities;
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
}