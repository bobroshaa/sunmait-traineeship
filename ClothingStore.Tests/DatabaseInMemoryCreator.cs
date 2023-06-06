using System.Security.Cryptography;
using System.Text;
using Bogus;
using ClothingStore.Domain.Entities;
using ClothingStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Tests;

public class DatabaseInMemoryCreator
{
    public static Context Create()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new Context(options);

        context.Database.EnsureCreated();

        var product = new Faker<Product>()
            .RuleFor(p => p.ID, 1)
            .RuleFor(p => p.AddDate, f => f.Date.Recent())
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
            .RuleFor(p => p.ImageURL, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Name, f => f.Lorem.Word())
            .RuleFor(p => p.Price, f => f.Random.Int(1, 500))
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 500))
            .RuleFor(p => p.IsActive, true);

        var user = new Faker<UserAccount>()
            .RuleFor(u => u.ID, 1)
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Password,
                f => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(f.Internet.Password()))))
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.IsActive, true);

        context.Products.Add(product);
        context.Users.Add(user);

        context.SaveChanges();

        return context;
    }
}