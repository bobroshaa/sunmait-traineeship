using System.Security.Cryptography;
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

        context.Products.Add(
            new Product
            {
                ID = 1,
                AddDate = DateTime.UtcNow,
                Description = "Pretty white dress with flowers.",
                ImageURL = "https://image",
                Name = "White Dress",
                Price = 10,
                Quantity = 10,
                IsActive = true
            }
        );
        context.Users.Add(
            new UserAccount
            {
                ID = 1,
                Email = "email@gmail.com",
                FirstName = "Ryan",
                LastName = "Gosling",
                Password = Convert.ToHexString(SHA256.HashData("password"u8.ToArray())),
                Phone = "88005553535",
                IsActive = true
            }
        );

        context.SaveChanges();

        return context;
    }
}