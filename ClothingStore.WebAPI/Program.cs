using ClothingStore.Application;
using ClothingStore.Application.Profiles;
using ClothingStore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplicationDependencies()
        .AddInfrastructure();
    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(typeof(BrandProfile), typeof(ProductProfile), typeof(OrderProfile),
        typeof(OrderItemProfile));
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();