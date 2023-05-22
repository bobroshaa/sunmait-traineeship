using ClothingStore.Application;
using ClothingStore.Application.Profiles;
using ClothingStore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure();
    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(typeof(BrandProfile));
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